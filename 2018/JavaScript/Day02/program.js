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

Array.prototype.take = function (n) {
  return this.slice(0, n);
};

Array.prototype.zip = function (other) {
  return this.take(Math.min(this.length, other.length))
    .map((x, i) => [x, other[i]]);
};

function charCounts(str) {
  return str.split('').countBy(x => x);
}

function calcChecksum(codes) { 
  var counts = codes.map(charCounts);
  var codesWithExactly2 = counts.filter(x => Object.values(x).includes(2));
  var codesWithExactly3 = counts.filter(x => Object.values(x).includes(3));
  return codesWithExactly2.length * codesWithExactly3.length;
}

function findCommonLetters(codes) { 
  function diffBy1(code1, code2) {
    const numberDifferent = code1.split('').zip(code2.split(''))
      .map(([letter1, letter2]) => letter1 === letter2)
      .filter(areEqual => !areEqual)
      .length
    return numberDifferent === 1;
  }

  function findFabricBoxCodes(codes, found) {
    if (codes.length < 2) {
      return found;
    } else {
      const [code, ...rest] = codes;
      rest.forEach(otherCode => {
        if (diffBy1(code, otherCode)) {
          found.add(code);
          found.add(otherCode);
        }
      });
      return findFabricBoxCodes(rest, found);
    }
  }

  const fabricCodes = findFabricBoxCodes(codes, new Set());
  const [code1, code2] = Array.from(fabricCodes);
  return code1.split('').zip(code2.split(''))
    .filter(([letter1, letter2]) => letter1 === letter2)
    .map(([letter, _]) => letter)
    .join('');
}

function test() {
  console.log("'ababab'.split('').groupBy(x => x) has b : [b,b,b]",
    'ababab'.split('').groupBy(x => x)['b'].every(x => x === 'b'));

  console.log("'ababab'.split('').groupBy(x => x) has b : 3",
    'ababab'.split('').countBy(x => x)['b'] === 3,
    'ababab'.split('').countBy(x => x));
  
  var counts = charCounts('ababab');
  console.log("'ababab' has 3 a's: ", counts.a === 3);
  console.log("'ababab' has 3 b's: ", counts.b === 3);

  var counts = charCounts('abbcde');
  console.log("'abbcde' has 2 b's: ", counts.b === 2);

  var codes = ['abcdef', 'bababc', 'abbcde', 'abcccd', 'aabcdd', 'abcdee', 'ababab'];
  var checksum = calcChecksum(codes)
  console.log("checksum for codes is 12: ", checksum === 12);

  codes = ["abcde","fghij","klmno","pqrst","fguij","axcye","wvxyz"];
  console.log("Common letters are 'fgij'?  ",
    findCommonLetters(codes) === 'fgij');
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
  console.log('What letters are common between the two correct box IDs? ',
    findCommonLetters(input));
}
