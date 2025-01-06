"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.activate = activate;
exports.deactivate = deactivate;
// The module 'vscode' contains the VS Code extensibility API
// Import the module and reference it with the alias vscode in your code below
const vscode = require("vscode");
const chokidar = require("chokidar");
function activate(context) {
    console.log('.Net Test Runner is active');
    let disposable = vscode.commands.registerCommand('testRunner.watchDirectory', () => {
        vscode.window.showInputBox({
            prompt: "Enter the directory path to watch",
            placeHolder: "e.g., /path/to/directory"
        }).then((directory) => {
            if (directory) {
                const watcher = chokidar.watch(directory, {
                    ignored: /(^|[\/\\])\../,
                    persistent: true
                });
                watcher.on('add', (path) => {
                    vscode.workspace.openTextDocument(path).then((doc) => {
                        vscode.window.showTextDocument(doc);
                    });
                });
                // Handle cleanup when extension is deactivated
                context.subscriptions.push({
                    dispose: () => {
                        watcher.close();
                    }
                });
            }
        });
    });
    context.subscriptions.push(disposable);
}
// This method is called when your extension is deactivated
function deactivate() { }
//# sourceMappingURL=extension.js.map