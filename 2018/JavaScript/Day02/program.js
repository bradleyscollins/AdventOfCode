#!/usr/bin/env osascript -l JavaScript

var app = Application.currentApplication();
app.includeStandardAdditions = true;

Array.prototype.groupBy = function (f) {
  return this.reduce((acc, x) => {
    var key = f(x);
    var group = acc[key] || [];
    acc[key] = [...group, x];
    return acc;
  }, {});
};

Array.prototype.countBy = function (f) {
  const grouped = this.groupBy(f);
  return Object.keys(grouped).reduce((acc, key) => {
    acc[key] = grouped[key].length;
    return acc;
  }, {});
};

function charCounts(str) {
  // return str.split('').reduce((acc, char) => {
  //   const count = acc[char] || 0;
  //   acc[char] = count + 1;
  //   return acc;
  // }, {});
  return str.split('').countBy(x => x);
}

function calcChecksum(codes) { 
  var counts = codes.map(charCounts);
  var codesWithExactly2 = counts.filter(x => Object.values(x).includes(2));
  var codesWithExactly3 = counts.filter(x => Object.values(x).includes(3));
  return codesWithExactly2.length * codesWithExactly3.length;
}

function test() {
  var counts = charCounts('ababab');
  console.log("'ababab' has 3 a's: ", counts.a === 3);
  console.log("'ababab' has 3 b's: ", counts.b === 3);

  var counts = charCounts('abbcde');
  console.log("'abbcde' has 2 b's: ", counts.b === 2);

  var codes = ['abcdef', 'bababc', 'abbcde', 'abcccd', 'aabcdd', 'abcdee', 'ababab'];
  var checksum = calcChecksum(codes)
  console.log("checksum for codes is 12: ", checksum === 12);

  console.log("'ababab'.split('').groupBy(x => x) has b : [b,b,b]",
    'ababab'.split('').groupBy(x => x)['b'].every(x => x === 'b'));

    console.log("'ababab'.split('').groupBy(x => x) has b : 3",
    'ababab'.split('').countBy(x => x)['b'] === 3,
    'ababab'.split('').countBy(x => x));
}

function run(argv) {
  console.log('Testing ...');

  test();


  console.log("Running ...");

  var input = app.read(Path('./input.txt'))
                .split('\n')
                .map(x => x.trim())
                .filter(x => x != '');

  console.log('What is the checksum? ', calcChecksum(input));
}
