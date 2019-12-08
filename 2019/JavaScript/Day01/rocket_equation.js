const calcFuelForMass = mass => {
  const fuel = Math.floor(mass / 3) - 2;
  return Math.max(fuel, 0);
};

const calcFuelForModule = moduleMass => {
  const calcFuelForMassRecusive = (mass, totalFuel) => {
    const fuel = calcFuelForMass(mass);

    return fuel <= 0
      ? totalFuel
      : calcFuelForMassRecusive(fuel, totalFuel + fuel);
  };

  return calcFuelForMassRecusive(moduleMass, 0);
};

const naiveRocketEquation = masses => {
  const fuels = masses.map(calcFuelForMass);
  return fuels.reduce((x, y) => x + y,  0);
};

const rocketEquation = masses => {
  const fuels = masses.map(calcFuelForModule);
  return fuels.reduce((x, y) => x + y,  0);
};

exports.calcFuelForMass = calcFuelForMass;
exports.calcFuelForModule = calcFuelForModule;
exports.naiveRocketEquation = naiveRocketEquation;
exports.rocketEquation = rocketEquation;
