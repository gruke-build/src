// Copyright 2020 Maintainers of NUKE.
// Copyright 2026 Maintainers of GRUKE.
// Distributed under the MIT License.
// https://github.com/gruke-build/src/blob/master/LICENSE

using System;
using System.Linq;
using Azure.Security.KeyVault.Keys;

namespace Nuke.Common.Tools.AzureKeyVault
{
    public class AzureKeyVaultKey
    {
        public JsonWebKey Key { get; internal set; }
        public string Secret { get; internal set; }
    }
}
