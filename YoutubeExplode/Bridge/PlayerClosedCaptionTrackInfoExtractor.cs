﻿using System;
using System.Linq;
using System.Text.Json;
using YoutubeExplode.Utils;
using YoutubeExplode.Utils.Extensions;

namespace YoutubeExplode.Bridge;

internal class PlayerClosedCaptionTrackInfoExtractor
{
    private readonly JsonElement _content;

    public PlayerClosedCaptionTrackInfoExtractor(JsonElement content) => _content = content;

    public string? TryGetUrl() => Memo.Cache(this, () =>
        _content
            .GetPropertyOrNull("baseUrl")?
            .GetStringOrNull()
    );

    public string? TryGetLanguageCode() => Memo.Cache(this, () =>
        _content
            .GetPropertyOrNull("languageCode")?
            .GetStringOrNull()
    );

    public string? TryGetLanguageName() => Memo.Cache(this, () =>
        _content
            .GetPropertyOrNull("name")?
            .GetPropertyOrNull("simpleText")?
            .GetStringOrNull() ??

        _content
            .GetPropertyOrNull("name")?
            .GetPropertyOrNull("runs")?
            .EnumerateArrayOrNull()?
            .Select(j => j.GetPropertyOrNull("text")?.GetStringOrNull())
            .WhereNotNull()
            .ConcatToString()
    );

    public bool IsAutoGenerated() => Memo.Cache(this, () =>
        _content
            .GetPropertyOrNull("vssId")?
            .GetStringOrNull()?
            .StartsWith("a.", StringComparison.OrdinalIgnoreCase) ?? false
    );
}