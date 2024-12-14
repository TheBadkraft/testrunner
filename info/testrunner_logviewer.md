
Project Purpose
The purpose of this extension is to provide a seamless experience for developers by automatically launching a log viewer within VS Code whenever a test suite completes. It will:

Read logs generated in JSON format by an existing JsonLoggingStrategy.
Apply custom Markdown annotations from a .jma file for enhanced log readability.
Display logs in a custom webview panel with formatted output.

Project Outline
Project Name:VSCode TestRunner.LogViewer

Directory Structure:
VSCodeTestRunner.LogViewer/
├── src/
│   ├── extension.ts          # Main entry point for the extension
│   ├── logReader.ts          # Module to handle reading JSON log files
│   ├── annotationStrategy.ts # Handling .jma annotations
│   ├── ui.tsx                # UI components for webview
│   ├── logViewer.ts          # Logic for displaying formatted logs
│   └── utils.ts              # Utility functions
├── test/
│   ├── testLog.sample.json   # Sample log file for testing
│   └── extension.test.ts     # Unit tests for the extension functionality
├── package.json              # Extension manifest
├── tsconfig.json             # TypeScript configuration
├── .vscode/
│   └── launch.json           # VS Code launch config for debugging extension
├── README.md                 # Project documentation
└── .gitignore

Setup
Initialize Project:
bash
npm init -y
npm install --save-dev @types/node @types/vscode typescript
TypeScript Configuration (tsconfig.json):
json
{
  "compilerOptions": {
    "module": "commonjs",
    "target": "es6",
    "outDir": "out",
    "lib": ["es6"],
    "sourceMap": true,
    "rootDir": "src",
    "strict": true
  },
  "exclude": ["node_modules", ".vscode-test"]
}

Code Candidates
package.json
json
{
  "name": "vscode-testrunner-logviewer",
  "description": "VS Code extension to view test logs with JSON and Markdown annotations",
  "version": "0.0.1",
  "main": "./out/extension.js",
  "activationEvents": [
    "onCommand:testRunnerLogViewer.showLog",
    "onLanguage:json" // Assuming logs are JSON files
  ],
  "contributes": {
    "commands": [{
      "command": "testRunnerLogViewer.showLog",
      "title": "Show Test Log"
    }],
    "languages": [{
      "id": "jma",
      "aliases": ["JMA", "json-markdown-annotation"],
      "extensions": [".jma"]
    }]
  },
  "scripts": {
    "vscode:prepublish": "npm run compile",
    "compile": "tsc -p ./",
    "watch": "tsc -watch -p ./",
    "test": "node ./node_modules/vscode/bin/test"
  },
  "devDependencies": {
    "@types/node": "^14.0.0",
    "@types/vscode": "^1.60.0",
    "typescript": "^4.0.0"
  }
}

src/extension.ts
typescript
import * as vscode from 'vscode';
import { readLogFile } from './logReader';
import { AnnotationStrategy } from './annotationStrategy';
import { createLogView } from './logViewer';

export function activate(context: vscode.ExtensionContext) {
    console.log('VSCode TestRunner.LogViewer is now active!');

    // Assume logs are in workspace root or specific path
    const logFilePath = vscode.workspace.workspaceFolders?.[0].uri.fsPath + '/test-output.json';
    const annotationPath = vscode.workspace.workspaceFolders?.[0].uri.fsPath + '/log-annotations.jma';

    const logFileWatcher = vscode.workspace.createFileSystemWatcher(logFilePath, false, false, false);
    
    logFileWatcher.onDidChange(() => {
        // Log file changed, read and display logs
        readLogFile(logFilePath).then(logEntries => {
            const annotationStrategy = new AnnotationStrategy(annotationPath);
            const annotations = annotationStrategy.getAnnotations();
            createLogView(logEntries, annotations);
        });
    });

    context.subscriptions.push(logFileWatcher);

    let disposable = vscode.commands.registerCommand('testRunnerLogViewer.showLog', () => {
        // Manual triggering of log viewer
        readLogFile(logFilePath).then(logEntries => {
            const annotationStrategy = new AnnotationStrategy(annotationPath);
            const annotations = annotationStrategy.getAnnotations();
            createLogView(logEntries, annotations);
        });
    });

    context.subscriptions.push(disposable);
}

export function deactivate() {}

src/logReader.ts
typescript
import * as vscode from 'vscode';

export interface LogEntry {
  timestamp: string;
  level: string;
  message: string;
}

export function readLogFile(filePath: string): Promise<LogEntry[]> {
  return vscode.workspace.fs.readFile(vscode.Uri.file(filePath)).then(buffer => {
    const jsonContent = Buffer.from(buffer).toString('utf8');
    const parsedJson = JSON.parse(jsonContent);
    return parsedJson.logEntries as LogEntry[];
  });
}

src/annotationStrategy.ts
typescript
import * as fs from 'fs';

export class AnnotationStrategy {
  private annotationFilePath: string;

  constructor(annotationFilePath: string) {
    this.annotationFilePath = annotationFilePath;
  }

  public getAnnotations(): any[] {
    try {
      const content = fs.readFileSync(this.annotationFilePath, 'utf8');
      return JSON.parse(content).annotations;
    } catch (error) {
      return []; // If no annotations file exists, return empty array
    }
  }
}

src/logViewer.ts
typescript
import * as vscode from 'vscode';
import { LogEntry } from './logReader';

export function createLogView(logEntries: LogEntry[], annotations: any[]) {
  const panel = vscode.window.createWebviewPanel(
    'testRunnerLogViewer',
    "Test Runner Log Viewer",
    vscode.ViewColumn.One,
    {}
  );

  const formattedLogs = logEntries.map(entry => {
    let formatted = `${entry.timestamp} ${entry.level} ${entry.message}`;
    if (annotations.length > 0) {
      const annotation = annotations[0];
      formatted = `${annotation.timestamp[0]}${entry.timestamp}${annotation.timestamp[1]} ${annotation.level[0]}${entry.level}${annotation.level[1]} ${annotation.message[0]}${entry.message}${annotation.message[1]}`;
    }
    return `<p>${formatted}</p>`;
  }).join('');

  panel.webview.html = getWebviewContent(formattedLogs);
}

function getWebviewContent(htmlContent: string) {
  return `<!DOCTYPE html>
  <html lang="en">
  <head>
      <meta charset="UTF-8">
      <meta name="viewport" content="width=device-width, initial-scale=1.0">
      <title>Test Log Viewer</title>
  </head>
  <body>
      ${htmlContent}
  </body>
  </html>`;
}

Assumption:
Your JsonLoggingStrategy (implemented in C#) is already creating JSON log files when tests conclude.

Final Steps:
Implement the rest of the TypeScript files like ui.tsx if using React for webviews.
Test the extension by simulating log file changes or using your actual test runner.
Document thoroughly in README.md.
Debug and refine based on feedback or additional requirements.
