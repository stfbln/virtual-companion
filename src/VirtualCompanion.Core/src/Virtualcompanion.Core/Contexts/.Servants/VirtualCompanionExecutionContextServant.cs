using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using Virtualcompanion.Core.Contexts.Features;
using Virtualcompanion.Core.Contexts.Features.Audios;

namespace Virtualcompanion.Core.Contexts
{
    public static class VirtualCompanionExecutionContextServant
    {
        public static void AddAudioInputFeature(this IVirtualCompanionExecutionContext context, string culture, byte[] buffer)
        {
            var cultureInfo = CultureInfo.GetCultureInfo(culture);
            context.AddAudioInputFeature(cultureInfo, buffer);
        }

        public static void AddAudioInputFeature(this IVirtualCompanionExecutionContext context, CultureInfo culture, byte[] buffer)
        {
            var featureKey = context.Configuration.AudioInputFeature.ContextKeyFactory(culture);

            context[featureKey] = new AudioInputFeature {
                Culture = culture,
                Buffer = buffer
            };
        }

        public static bool TryGetAudioInputFeature(this IVirtualCompanionExecutionContext context, CultureInfo culture, out byte[] buffer)
        {
            var featureKey = context.Configuration.AudioInputFeature.ContextKeyFactory(culture);

            buffer = new byte[0];
            if (context.TryGetFeature<AudioInputFeature>(featureKey, out var feature))
            {
                buffer = feature.Buffer;
            }

            return buffer.Length > 0;
        }

        public static bool TryGetAudioInputFeature(this IVirtualCompanionExecutionContext context, string culture, out byte[] buffer)
        {
            var cultureInfo = CultureInfo.GetCultureInfo(culture);
            return context.TryGetAudioInputFeature(cultureInfo, out buffer);

        }

        public static void AddTextInputFeature(this IVirtualCompanionExecutionContext context, string culture, string text)
        {
            var cultureInfo = CultureInfo.GetCultureInfo(culture);
            context.AddTextInputFeature(cultureInfo, text);
        }

        public static void AddTextInputFeature(this IVirtualCompanionExecutionContext context, CultureInfo culture, string text)
        {
            var featureKey = context.Configuration.TextInputFeature.ContextKeyFactory(culture);
            
            context[featureKey] = new TextInputFeature {
                Culture = culture,
                Text = text
            };
        }

        public static bool TryGetTextInputFeature(this IVirtualCompanionExecutionContext context, CultureInfo culture, out string text)
        {
            var featureKey = context.Configuration.TextInputFeature.ContextKeyFactory(culture);
            
            text = default(string);
            if (context.TryGetFeature<TextInputFeature>(featureKey, out var feature))
            {
                text = feature.Text;
            }

            return text != null;
        }

        public static bool TryGetTextInputFeature(this IVirtualCompanionExecutionContext context, string culture, out string text)
        {
            var cultureInfo = CultureInfo.GetCultureInfo(culture);
            return context.TryGetTextInputFeature(cultureInfo, out text);

        }

        public static bool TryGetFeature<TFeature>(this IVirtualCompanionExecutionContext context, string key, out TFeature feature)
        {
            feature = default(TFeature);
            if (context.TryGetValue(key, out var value) && value is TFeature castedFeature)
            {
                feature = castedFeature;
            }

            return !Object.Equals(feature, default(TFeature));
        }

        public static IEnumerable<TFeature> GetFeatures<TFeature>(this IVirtualCompanionExecutionContext context)
        {
            return context.Values.Where((v) => v is TFeature).Cast<TFeature>();
        }
    }
}
