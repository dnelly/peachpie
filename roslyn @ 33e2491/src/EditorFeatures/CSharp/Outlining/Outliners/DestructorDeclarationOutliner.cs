// Copyright (c) Microsoft.  All Rights Reserved.  Licensed under the Apache License, Version 2.0.  See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Threading;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Editor.Implementation.Outlining;

namespace Microsoft.CodeAnalysis.Editor.CSharp.Outlining
{
    internal class DestructorDeclarationOutliner : AbstractSyntaxNodeOutliner<DestructorDeclarationSyntax>
    {
        protected override void CollectOutliningSpans(
            DestructorDeclarationSyntax destructorDeclaration,
            List<OutliningSpan> spans,
            CancellationToken cancellationToken)
        {
            CSharpOutliningHelpers.CollectCommentRegions(destructorDeclaration, spans);

            // fault tolerance
            if (destructorDeclaration.Body == null ||
                destructorDeclaration.Body.OpenBraceToken.IsMissing ||
                destructorDeclaration.Body.CloseBraceToken.IsMissing)
            {
                return;
            }

            spans.Add(CSharpOutliningHelpers.CreateRegion(
                destructorDeclaration,
                destructorDeclaration.ParameterList.GetLastToken(includeZeroWidth: true),
                autoCollapse: true));
        }
    }
}
