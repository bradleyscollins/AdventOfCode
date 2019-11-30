const assert = require('assert');
const {
  toDomain,
  buildGuardReposeMap,
  calcTotalSleepTime,
  buildSleepHistogram,
  calcBestChance
} = require('../repose_record');

describe("toDomain", function () {
  describe("given a shift change record", function () {
    it("returns a guard record", function () {
      const actual = toDomain('[1518-11-05 00:03] Guard #99 begins shift');
      assert.deepEqual(actual, { guard: '99' });
    });
  });

  describe("given a sleep record", function () {
    it("returns a sleep record", function () {
      const actual = toDomain('[1518-11-04 00:36] falls asleep');
      assert.deepEqual(actual, { sleep: 36 });
    });
  });

  describe("given a wake record", function () {
    it("returns a wake record", function () {
      const actual = toDomain('[1518-11-02 00:50] wakes up');
      assert.deepEqual(actual, { wake: 50 });
    });
  });
});

describe("buildGuardReposeMap", function () {
  describe("given a list of domain records", function () {
    it("builds a map of guard IDs to sleep-wake records", function () {
      const domainRecords = [
        { guard: '10' }, { sleep:  5 }, { wake: 25 },
                         { sleep: 30 }, { wake: 55 },
        { guard: '99' }, { sleep: 40 }, { wake: 50 }, 
        { guard: '10' }, { sleep: 24 }, { wake: 29 },
        { guard: '99' }, { sleep: 36 }, { wake: 46 },
        { guard: '99' }, { sleep: 45 }, { wake: 55 },
      ];

      const expected = {
        '10': [
          { guard: '10', sleep:  5, wake: 25 },
          { guard: '10', sleep: 30, wake: 55 },
          { guard: '10', sleep: 24, wake: 29 },
        ],
        '99': [
          { guard: '99', sleep: 40, wake: 50 }, 
          { guard: '99', sleep: 36, wake: 46 },
          { guard: '99', sleep: 45, wake: 55 },
        ],
      };

      const actual = buildGuardReposeMap(domainRecords);

      assert.deepEqual(actual, expected);
    });
  });
});

describe("calcTotalSleepTime", function () {
  describe("given a list of sleep-wake records", function () {
    it("calculates the total time a guard has slept", function () {
      const records = [
        { sleep:  5, wake: 25 },
        { sleep: 30, wake: 55 },
        { sleep: 24, wake: 29 },
      ];

      assert.equal(calcTotalSleepTime(records), 50);
    });
  });
});

describe("buildSleepHistogram", function () {
  describe("given a list of sleep-wake records", function () {
    it("builds a histogram showing the number of times asleep on a given minute", function () {
      const records = [
        { sleep:  5, wake: 25 },
        { sleep: 30, wake: 55 },
        { sleep: 24, wake: 29 },
      ];

      const expected = [];
      expected[ 5] = 1;
      expected[ 6] = 1;
      expected[ 7] = 1;
      expected[ 8] = 1;
      expected[ 9] = 1;
      expected[10] = 1;
      expected[11] = 1;
      expected[12] = 1;
      expected[13] = 1;
      expected[14] = 1;
      expected[15] = 1;
      expected[16] = 1;
      expected[17] = 1;
      expected[18] = 1;
      expected[19] = 1;
      expected[20] = 1;
      expected[21] = 1;
      expected[22] = 1;
      expected[23] = 1;
      expected[24] = 2;
      expected[25] = 1;
      expected[26] = 1;
      expected[27] = 1;
      expected[28] = 1;
      expected[30] = 1;
      expected[31] = 1;
      expected[32] = 1;
      expected[33] = 1;
      expected[34] = 1;
      expected[35] = 1;
      expected[36] = 1;
      expected[37] = 1;
      expected[38] = 1;
      expected[39] = 1;
      expected[40] = 1;
      expected[41] = 1;
      expected[42] = 1;
      expected[43] = 1;
      expected[44] = 1;
      expected[45] = 1;
      expected[46] = 1;
      expected[47] = 1;
      expected[48] = 1;
      expected[49] = 1;
      expected[50] = 1;
      expected[51] = 1;
      expected[52] = 1;
      expected[53] = 1;
      expected[54] = 1;

      assert.deepEqual(buildSleepHistogram(records), expected);
    });
  });
});

describe("calcBestChance", function () {
  describe("given an unsorted list of sleep/wake records", function () {
    it("determines the guard and the minute we have the best chance of sneaking in", function () {
      const unsortedSleepWakeRecords = [
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

      const actual = calcBestChance(unsortedSleepWakeRecords);

      assert.deepEqual(actual, { guard: '10', minute: 24 });
    });
  });
});
