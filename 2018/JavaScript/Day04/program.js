const assert = require('assert');

Array.prototype.groupBy = function (f) {
  return this.reduce((grouped, x) => {
    var key = f(x);
    var group = grouped[key] || [];
    grouped[key] = [...group, x];
    return grouped;
  }, {});
};

Number.prototype.until = function (last) {
  const range = [];
  for (let x = Number(this); x < last; x++) range.push(x);
  return range;
};

const originalInput = Object.freeze([
  '[1518-11-01 00:00] Guard #10 begins shift',
  '[1518-11-01 00:05] falls asleep',
  '[1518-11-01 00:25] wakes up',
  '[1518-11-01 00:30] falls asleep',
  '[1518-11-01 00:55] wakes up',
  '[1518-11-01 23:58] Guard #99 begins shift',
  '[1518-11-02 00:40] falls asleep',
  '[1518-11-02 00:50] wakes up',
  '[1518-11-03 00:05] Guard #10 begins shift',
  '[1518-11-03 00:24] falls asleep',
  '[1518-11-03 00:29] wakes up',
  '[1518-11-04 00:02] Guard #99 begins shift',
  '[1518-11-04 00:36] falls asleep',
  '[1518-11-04 00:46] wakes up',
  '[1518-11-05 00:03] Guard #99 begins shift',
  '[1518-11-05 00:45] falls asleep',
  '[1518-11-05 00:55] wakes up',
]);

const input = [
  '[1518-11-02 00:50] wakes up',
  '[1518-11-03 00:05] Guard #10 begins shift',
  '[1518-11-03 00:24] falls asleep',
  '[1518-11-03 00:29] wakes up',
  '[1518-11-04 00:02] Guard #99 begins shift',
  '[1518-11-04 00:36] falls asleep',
  '[1518-11-04 00:46] wakes up',
  '[1518-11-05 00:03] Guard #99 begins shift',
  '[1518-11-05 00:45] falls asleep',
  '[1518-11-05 00:55] wakes up',
  '[1518-11-01 00:00] Guard #10 begins shift',
  '[1518-11-01 00:05] falls asleep',
  '[1518-11-01 00:25] wakes up',
  '[1518-11-01 00:30] falls asleep',
  '[1518-11-01 00:55] wakes up',
  '[1518-11-01 23:58] Guard #99 begins shift',
  '[1518-11-02 00:40] falls asleep',
];

var sorted = Array.from(input).sort();

assert.deepEqual(sorted, originalInput);

function transform(line) {
  const guardPattern = /\[\d\d\d\d-\d\d-\d\d \d\d:\d\d\] Guard #(\d+) begins shift/;
  const sleepPattern = /\[\d\d\d\d-\d\d-\d\d \d\d:(\d\d)\] falls asleep/;
  const wakePattern = /\[\d\d\d\d-\d\d-\d\d \d\d:(\d\d)\] wakes up/;

  var result;

  result = line.match(guardPattern);
  if (result) return { guard: result[1] };

  result = line.match(sleepPattern);
  if (result) return { sleep: parseInt(result[1]) };
  
  result = line.match(wakePattern);
  if (result) return { wake: parseInt(result[1]) };
  
  return null;
}

const transformed = sorted.map(transform);
console.info(transformed);

const guards = transformed
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
    { records: [] })
  .records
  .groupBy(record => record.guard);

console.log(guards);

assert.deepEqual(Object.keys(guards).sort(), ['10', '99']);

const guardSleepTimes = Object.keys(guards).map(guard => {
  const asleep = guards[guard].map(({ sleep, wake }) => wake - sleep)
                              .reduce((x,y) => x + y)
  return { guard, asleep };
});

console.log(guardSleepTimes);

assert.deepEqual(guardSleepTimes.sort((x,y) => y.asleep - x.asleep),
  [ { guard: '10', asleep: 50 }, { guard: '99', asleep: 30 } ]);

const sleepiestGuard = guardSleepTimes
  .reduce((x,y) => x.asleep > y.asleep ? x : y)
  .guard;

assert.equal(sleepiestGuard, '10');

const sleepHistogram = guards[sleepiestGuard]
  .flatMap(({ sleep, wake }) => sleep.until(wake))
  .reduce((histogram, minute) => {
    histogram[minute] = (histogram[minute] || 0) + 1;
    return histogram;
  }, []);

const mostLikelyMinute = sleepHistogram
  .reduce(
    (max, numberOfTimesAsleep, minute) => {
      return numberOfTimesAsleep > max.numberOfTimesAsleep
        ? { numberOfTimesAsleep, minute }
        : max;
    },
    { numberOfTimesAsleep: 0, minute: 0 })
  .minute;

assert.equal(mostLikelyMinute, 24);

console.log(`Guard ${sleepiestGuard} at minute ${mostLikelyMinute}`);
