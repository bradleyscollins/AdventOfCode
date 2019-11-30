const fs = require('fs');
const { calcBestChance } = require('./repose_record');

const input = fs.readFileSync('./input.txt', 'utf8')
  .toString()
  .split(/\r?\n/)
  .map(line => line.trim())
  .filter(line => line.length > 0);

const { guard, minute } = calcBestChance(input);

console.log(`The best chance to sneak in is right at 00:${minute} when guard ${guard} is on duty`);
console.log('The answer for part 1:', parseInt(guard) * minute);
