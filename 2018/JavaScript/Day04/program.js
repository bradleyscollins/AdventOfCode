const assert = require('assert');

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

function transform(record) {
  const guardPattern = /\[\d\d\d\d-\d\d-\d\d \d\d:\d\d\] Guard #(\d+) begins shift/;
  const sleepPattern = /\[\d\d\d\d-\d\d-\d\d \d\d:(\d\d)\] falls asleep/;
  const wakePattern = /\[\d\d\d\d-\d\d-\d\d \d\d:(\d\d)\] wakes up/;

  var result;

  result = record.match(guardPattern);
  if (result) return { guard: result[1] };

  result = record.match(sleepPattern);
  if (result) return { sleep: parseInt(result[1]) };
  
  result = record.match(wakePattern);
  if (result) return { wake: parseInt(result[1]) };
  
  return null;
}

assert.deepEqual(sorted, originalInput);

const transformed = sorted.map(transform);
console.info(transformed);

const guards = transformed.reduce(
  (guardData, { guard, sleep, wake }) => {
    if (guard) {
      const guards = guardData.guards;
      guards[guard] = guards[guard] || [];
      return { guards, guard };
    } else if (sleep) {
      return { ...guardData, sleep };
    } else {
      const { guards, guard, sleep } = guardData;
      guards[guard].push({ guard, sleep, wake });
      return { guards, guard };
    }
  },
  { guards: {} }
).guards;

console.log(guards);

const guardSleepTimes = Object.keys(guards).map(guard => {
  const asleep = guards[guard].map(({ sleep, wake }) => wake - sleep)
                              .reduce((x,y) => x + y)
  return { guard, asleep };
});

console.log(guardSleepTimes);
