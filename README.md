# TreeViewWalker

A lightweight .NET class library that simplifies recursive traversal of Windows Forms `TreeView` controls using an event-driven visitor pattern.

**Source last updated:** 2020-04-23
**Initiated:** 2006-02-21 · **Framework:** .NET Framework 4.8 · **Output:** Class Library

> Originally published on CodeProject: http://www.codeproject.com/Articles/12952/TreeViewWalker-Simplifying-Recursion

---

## Usage

```csharp
var walker = new TreeViewWalker(myTreeView);

walker.ProcessNode += (sender, e) =>
{
    Console.WriteLine(e.Node.Text);
    // e.ProcessDescendants = false; // Skip children
    // e.ProcessSiblings    = false; // Skip remaining siblings
    // e.StopProcessing     = true;  // Abort the entire walk
};

walker.ProcessTree();           // Walk the entire TreeView
walker.ProcessBranch(someNode); // Walk a single subtree
```

---

## API

| Member | Description |
|--------|-------------|
| `ProcessTree()` | Walks all root nodes and their descendants |
| `ProcessBranch(TreeNode)` | Walks the given node and its descendants |
| `ProcessNode` event | Raised for every visited node |

### ProcessNodeEventArgs

| Property | Default | Description |
|----------|---------|-------------|
| `Node` | - | The current `TreeNode` |
| `ProcessDescendants` | `true` | Set `false` to skip child nodes |
| `ProcessSiblings` | `true` | Set `false` to skip remaining siblings |
| `StopProcessing` | `false` | Set `true` to abort the walk |

> `ProcessNodeEventArgs` is implemented as a singleton to avoid heap pressure on large trees.

## Solution structure

| Project | Language | Type | Purpose |
|---------|----------|------|---------|
| `TreeViewWalker` (`TreeViewWalker.csproj`) | C# | Class library (.NET Framework 4.8) | `TreeViewWalker` walker class plus `ProcessNodeEventArgs` in `Events.cs` |

## How to open

Open `TreeViewWalker.csproj` in Visual Studio (ToolsVersion 12.0) and build the library; reference `TreeViewWalker.dll` from a WinForms project. There is no `.sln` in this tree.

## Requirements

- Visual Studio 2013 or later, .NET Framework 4.8 Developer Pack

## Attribution and provenance

Working copy from my Historical Dev folder.

Working copy from my Development folder `TreeViewWalker`. The walker design and source comments point at the CodeProject article [TreeViewWalker - Simplifying Recursion](http://www.codeproject.com/Articles/12952/TreeViewWalker-Simplifying-Recursion); an older copy also sits in my Historical Dev folder `B4A_2/TreeViewWalker`.

## License

MIT © 2026 Vader Consulting for Dave Robinson's code. See `LICENSE`. Code taken from the CodeProject article keeps the article's original terms.
