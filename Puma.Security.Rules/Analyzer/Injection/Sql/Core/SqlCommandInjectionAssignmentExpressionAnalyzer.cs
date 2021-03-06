/* 
 * Copyright(c) 2016 - 2020 Puma Security, LLC (https://pumasecurity.io)
 * 
 * Project Leads:
 * Eric Johnson (eric.johnson@pumascan.com)
 * Eric Mead (eric.mead@pumascan.com)
 * 
 * This Source Code Form is subject to the terms of the Mozilla Public
 * License, v. 2.0. If a copy of the MPL was not distributed with this
 * file, You can obtain one at http://mozilla.org/MPL/2.0/. 
 */

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using Puma.Security.Rules.Analyzer.Core;
using Puma.Security.Rules.Common;

namespace Puma.Security.Rules.Analyzer.Injection.Sql.Core
{
    internal class SqlCommandInjectionAssignmentExpressionAnalyzer : ISqlCommandInjectionAssignmentExpressionAnalyzer
    {
        public bool IsVulnerable(SemanticModel model, AssignmentExpressionSyntax syntax, DiagnosticId ruleId)
        {
            var leftSyntax = syntax?.Left as MemberAccessExpressionSyntax;

            if (leftSyntax == null || leftSyntax.Name.Identifier.ValueText.ToLower() != "commandtext") return false;

            var leftSymbol = model.GetSymbolInfo(leftSyntax).Symbol;

            if (!(leftSymbol != null && (leftSymbol.ToString().StartsWith("System.Data.SqlClient.SqlCommand")) || leftSymbol.ToString().StartsWith("Microsoft.Data.Sqlite.SqliteCommand"))) 
                return false;

            var expressionAnalyzer = SyntaxNodeAnalyzerFactory.Create(syntax.Right);
            if (expressionAnalyzer.CanIgnore(model, syntax.Right))
                return false;
            if (expressionAnalyzer.CanSuppress(model, syntax.Right, ruleId))
                return false;

            return true;
        }
    }
}