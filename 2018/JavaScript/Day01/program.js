#!/usr/bin/env osascript -l JavaScript

var app = Application.currentApplication();
app.includeStandardAdditions = true;

function* infinite(xs) {
  const len = xs.length;
	for (let i = 0; true; i = (i + 1) % len) {
    yield xs[i];
  }
}

function calcResult1(changes) {
  return changes.reduce((a,b) => a + b, 0);
}

function calcResult2(frequencyChanges) {
  var infiniteChanges = infinite(frequencyChanges);
  var lastFrequency = 0;
  var previousFrequencies = new Set([lastFrequency]);
  for (var changeInFrequency of infiniteChanges) {
    var nextFrequency = lastFrequency + changeInFrequency;
    if (previousFrequencies.has(nextFrequency)) {
      return nextFrequency;
    } else {
      previousFrequencies.add(nextFrequency);
      lastFrequency = nextFrequency;
    }
  }
}

function run(argv) {
  console.log("Running ...");

  var input = app.read(Path('./input.txt'))
                .split('\n')
                .map(x => x.trim())
                .filter(x => x != '')
                .map(x => parseInt(x));

  console.log('Result 1:', calcResult1(input));
  console.log('Result 2:', calcResult2(input));
}
