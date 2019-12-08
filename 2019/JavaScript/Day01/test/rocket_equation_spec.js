const assert = require('assert');

const {
  calcFuelForMass,
  calcFuelForModule,
  naiveRocketEquation,
  rocketEquation,
} = require('../rocket_equation');

describe('calcFuelForMass', function () {
  const testCases = [
    [      0,     0 ],
    [      1,     0 ],
    [      2,     0 ],
    [      3,     0 ],
    [      4,     0 ],
    [      5,     0 ],
    [      6,     0 ],
    [     12,     2 ],
    [     14,     2 ],
    [   1969,   654 ],
    [ 100756, 33583 ],
  ];

  testCases.forEach(([mass, expected]) => {
    describe(`given a mass of ${mass}`, function () {
      it(`calculates a fuel value of ${expected}`, function () {
        const actual = calcFuelForMass(mass);
        assert.equal(actual, expected);
      });
    });
  });
});

describe('calcFuelForModule', function () {
  const testCases = [
    [      0,     0 ],
    [      1,     0 ],
    [      2,     0 ],
    [      3,     0 ],
    [      4,     0 ],
    [      5,     0 ],
    [      6,     0 ],
    [     12,     2 ],
    [     14,     2 ],
    [   1969,   966 ],
    [ 100756, 50346 ],
  ];

  testCases.forEach(([mass, expected]) => {
    describe(`given a module mass of ${mass}`, function () {
      it(`calculates a total fuel required to lift it of ${expected}`, function () {
        const actual = calcFuelForModule(mass);
        assert.equal(actual, expected);
      });
    });
  });
});

describe('naiveRocketEquation', function () {
  describe('given a list of masses', function () {
    it('calculates the fuel needed to lift all of those masses', function () {
      const masses = [ 12, 14, 1969, 100756 ];
      const totalFuel = naiveRocketEquation(masses);
      assert.equal(totalFuel, 34_241);
    });
  });
});

describe('rocketEquation', function () {
  describe('given a list of masses', function () {
    it('calculates the fuel needed to lift all of those masses and their fuel', function () {
      const masses = [ 12, 14, 1969, 100756 ];
      const totalFuel = rocketEquation(masses);
      assert.equal(totalFuel, 51_316);
    });
  });
});
