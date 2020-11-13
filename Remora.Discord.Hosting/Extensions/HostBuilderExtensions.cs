//
//  HostBuilderExtensions.cs
//
//  Author:
//       Jarl Gullberg <jarl.gullberg@gmail.com>
//
//  Copyright (c) 2017 Jarl Gullberg
//
//  This program is free software: you can redistribute it and/or modify
//  it under the terms of the GNU Lesser General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
//
//  This program is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU Lesser General Public License for more details.
//
//  You should have received a copy of the GNU Lesser General Public License
//  along with this program.  If not, see <http://www.gnu.org/licenses/>.
//

using System;
using JetBrains.Annotations;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Remora.Discord.Gateway.Extensions;
using Remora.Discord.Hosting.Services;

namespace Remora.Discord.Hosting.Extensions
{
    /// <summary>
    /// Defines extension methods for the <see cref="IHostBuilder"/> interface.
    /// </summary>
    [PublicAPI]
    public static class HostBuilderExtensions
    {
        /// <summary>
        /// Adds the required services for Remora Discord and a <see cref="IHostedService"/> implementation.
        /// </summary>
        /// <param name="hostBuilder">The host builder.</param>
        /// <param name="token">A function that retrieves the bot token.</param>
        /// <returns>The service collection, with the services added.</returns>
        public static IHostBuilder AddDiscordService(this IHostBuilder hostBuilder, Func<string> token)
        {
            hostBuilder.ConfigureServices((_, serviceCollection) =>
            {
                serviceCollection
                    .AddDiscordGateway(token)
                    .AddSingleton<DiscordService>()
                    .AddSingleton<IHostedService, DiscordService>(serviceProvider =>
                        serviceProvider.GetRequiredService<DiscordService>());
            });

            return hostBuilder;
        }
    }
}