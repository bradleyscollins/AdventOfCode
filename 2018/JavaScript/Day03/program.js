#!/usr/bin/env osascript -l JavaScript

var app = Application.currentApplication();
app.includeStandardAdditions = true;

function test() {
}

function run(argv) {
  console.log('Testing ...');

  test();


  console.log("Running ...");

  var input = app.read(Path('./input.txt'))
                .split('\n')
                .map(x => x.trim())
                .filter(x => x != '');

  console.log('How many square inches of fabric are within two or more claims? ',
    calOverlap(input));
  console.log("What is the ID of the only claim that doesn't overlap? ",
    findCommonLetters(input));
}
