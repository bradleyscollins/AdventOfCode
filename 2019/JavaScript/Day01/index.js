const fs = require('fs');
const { naiveRocketEquation, rocketEquation } = require('./rocket_equation');

const masses = fs.readFileSync('./input.txt', 'utf8')
  .toString()
  .split(/\r?\n/)
  .map(line => line.trim())
  .filter(line => line.length > 0)
  .map(line => parseInt(line));

const naiveTotalFuel = naiveRocketEquation(masses);
console.log(`What is the sum of the fuel requirements for all of the modules on your spacecraft?  ${naiveTotalFuel}`);

const realTotalFuel = rocketEquation(masses);
console.log(`What is the sum of the fuel requirements for all of the modules on your spacecraft AND their fuel?  ${realTotalFuel}`);
