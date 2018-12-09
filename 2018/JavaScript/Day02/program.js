#!/usr/bin/env osascript -l JavaScript

var app = Application.currentApplication();
app.includeStandardAdditions = true;

function run(argv) {
  console.log("Running ...");

  var input = app.read(Path('./input.txt'))
                .split('\n')
                .map(x => x.trim())
                .filter(x => x != '');

  // add code here
}
