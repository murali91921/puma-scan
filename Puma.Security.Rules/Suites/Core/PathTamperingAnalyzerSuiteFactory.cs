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

using System.Collections.Immutable;

using Puma.Security.Rules.Analyzer;
using Puma.Security.Rules.Analyzer.Validation.Path;

namespace Puma.Security.Rules.Suites.Core
{
    public class PathTamperingAnalyzerSuiteFactory : IAnalyzerSuiteFactory
    {
        public ImmutableArray<ICompilationAnalyzer> Create()
        {
            return new ICompilationAnalyzer[]
            {
                new FilePathResultAnalyzer(),
                new FileStreamAnalyzer(),
                new IOFileAnalyzer()
            }.ToImmutableArray();
        }
    }
}