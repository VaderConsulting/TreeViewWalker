# TreeViewWalker

A lightweight .NET class library that simplifies recursive traversal of Windows Forms `TreeView` controls using an event-driven visitor pattern.

**Source last updated:** 2020-04-22
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

## Requirements

- Visual Studio 2013 or later, .NET Framework 4.8

