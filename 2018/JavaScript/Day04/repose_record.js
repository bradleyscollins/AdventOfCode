Object.defineProperty(Array.prototype, 'sum', {
  get: function () {
    return this.reduce((x, y) => x + y, 0);
  },
});

Array.prototype.groupBy = function (f) {
  return this.reduce((grouped, x) => {
    var key = f(x);
    var group = grouped[key] || [];
    grouped[key] = [...group, x];
    return grouped;
  }, {});
};

Array.prototype.maxBy = function (f) {
  switch (this.length) {
    case 0: return undefined;
    case 1: return this[0];
    default: return this.reduce((x, y) => f(x) > f(y) ? x : y);
  }
};

Number.prototype.until = function (last) {
  const range = [];
  for (let x = Number(this); x < last; x++) range.push(x);
  return range;
};

const toDomain = text => {
  const guardPattern = /\[\d\d\d\d-\d\d-\d\d \d\d:\d\d\] Guard #(\d+) begins shift/;
  const sleepPattern = /\[\d\d\d\d-\d\d-\d\d \d\d:(\d\d)\] falls asleep/;
  const wakePattern = /\[\d\d\d\d-\d\d-\d\d \d\d:(\d\d)\] wakes up/;

  var result;

  result = text.match(guardPattern);
  if (result) return { guard: result[1] };

  result = text.match(sleepPattern);
  if (result) return { sleep: parseInt(result[1]) };
  
  result = text.match(wakePattern);
  if (result) return { wake: parseInt(result[1]) };
  
  return null;
};

const buildGuardReposeMap = domainRecords => {
  return domainRecords
    .reduce(
      (data, { guard, sleep, wake }) => {
        if (guard) {
          return { ...data, guard };
        } else if (sleep) {
          return { ...data, sleep };
        } else {
          const { records, guard, sleep } = data;
          const record = { guard, sleep, wake };
          return { ...data, records: [ ...records, record ] };
        }
      },
      { records: [], guard: null, sleep: null })
    .records
    .groupBy(record => record.guard);
};

const calcTotalSleepTime = sleepWakeRecords => {
  return sleepWakeRecords.map(({sleep, wake}) => wake - sleep).sum;
};

const buildSleepHistogram = sleepWakeRecords => {
  return sleepWakeRecords.flatMap(({ sleep, wake }) => sleep.until(wake))
    .reduce((histogram, minute) => {
      histogram[minute] = (histogram[minute] || 0) + 1;
      return histogram;
    }, []);
};

exports.toDomain = toDomain;
exports.buildGuardReposeMap = buildGuardReposeMap;
exports.calcTotalSleepTime = calcTotalSleepTime;
exports.buildSleepHistogram = buildSleepHistogram;

exports.calcBestChance = input => {
  const domainRecords = Array.from(input).sort().map(toDomain);
  
  const guardReposeMap = buildGuardReposeMap(domainRecords);
  
  const guardSleepTimes = Object.keys(guardReposeMap).map(guard => {
    const asleep = calcTotalSleepTime(guardReposeMap[guard]);
    return { guard, asleep };
  });

  const sleepiestGuard = guardSleepTimes.maxBy(x => x.asleep).guard;
  
  const sleepHistogram = buildSleepHistogram(guardReposeMap[sleepiestGuard]);
  
  const mostLikelyMinute = sleepHistogram
    .reduce(
      (max, numberOfTimesAsleep, minute) => {
        return numberOfTimesAsleep > max.numberOfTimesAsleep
          ? { numberOfTimesAsleep, minute }
          : max;
      },
      { numberOfTimesAsleep: 0, minute: 0 })
    .minute;
  
  return { guard: sleepiestGuard, minute: mostLikelyMinute };
};
