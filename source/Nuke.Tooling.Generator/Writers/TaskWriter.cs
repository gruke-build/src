// Copyright 2023 Maintainers of NUKE.
// Copyright 2026 Maintainers of GRUKE.
// Distributed under the MIT License.
// https://github.com/gruke-build/src/blob/master/LICENSE

using System;
using System.Linq;
using Nuke.CodeGeneration.Model;

namespace Nuke.CodeGeneration.Writers;

public class TaskWriter : IWriterWrapper
{
    public TaskWriter(Task task, ToolWriter toolWriter)
    {
        Task = task;
        Writer = toolWriter;
    }

    public Task Task { get; }
    public IWriter Writer { get; }
}
