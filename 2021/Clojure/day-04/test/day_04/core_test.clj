(ns day-04.core-test
  (:require [clojure.test :refer :all]
            [clojure.string  :as str]
            [day-04.core :refer :all]))

(def test-input-lines
  ["7,4,9,5,11,17,23,2,0,14,21,24,10,16,13,6,15,25,12,22,18,20,8,19,3,26,1"
   ""
   "22 13 17 11  0"
   " 8  2 23  4 24"
   "21  9 14 16  7"
   " 6 10  3 18  5"
   " 1 12 20 15 19"
   ""
   " 3 15  0  2 22"
   " 9 18 13 17  5"
   "19  8  7 25 23"
   "20 11 10 24  4"
   "14 21 16 12  6"
   ""
   "14 21 17 24  4"
   "10 16 15  9 19"
   "18  8 23 26 20"
   "22 11 13  6  5"
   " 2  0 12  3  7"])

(deftest board-row-test
  (testing "Converts an index to a row"
    (is (= 1 (board-row  0)))
    (is (= 1 (board-row  1)))
    (is (= 1 (board-row  2)))
    (is (= 1 (board-row  3)))
    (is (= 1 (board-row  4)))
    (is (= 2 (board-row  5)))
    (is (= 2 (board-row  6)))
    (is (= 2 (board-row  7)))
    (is (= 2 (board-row  8)))
    (is (= 2 (board-row  9)))
    (is (= 3 (board-row 10)))
    (is (= 3 (board-row 11)))
    (is (= 3 (board-row 12)))
    (is (= 3 (board-row 13)))
    (is (= 3 (board-row 14)))
    (is (= 4 (board-row 15)))
    (is (= 4 (board-row 16)))
    (is (= 4 (board-row 17)))
    (is (= 4 (board-row 18)))
    (is (= 4 (board-row 19)))
    (is (= 5 (board-row 20)))
    (is (= 5 (board-row 21)))
    (is (= 5 (board-row 22)))
    (is (= 5 (board-row 23)))
    (is (= 5 (board-row 24)))))

(deftest board-col-test
  (testing "Converts an index to a col"
    (is (= 1 (board-col  0)))
    (is (= 2 (board-col  1)))
    (is (= 3 (board-col  2)))
    (is (= 4 (board-col  3)))
    (is (= 5 (board-col  4)))
    (is (= 1 (board-col  5)))
    (is (= 2 (board-col  6)))
    (is (= 3 (board-col  7)))
    (is (= 4 (board-col  8)))
    (is (= 5 (board-col  9)))
    (is (= 1 (board-col 10)))
    (is (= 2 (board-col 11)))
    (is (= 3 (board-col 12)))
    (is (= 4 (board-col 13)))
    (is (= 5 (board-col 14)))
    (is (= 1 (board-col 15)))
    (is (= 2 (board-col 16)))
    (is (= 3 (board-col 17)))
    (is (= 4 (board-col 18)))
    (is (= 5 (board-col 19)))
    (is (= 1 (board-col 20)))
    (is (= 2 (board-col 21)))
    (is (= 3 (board-col 22)))
    (is (= 4 (board-col 23)))
    (is (= 5 (board-col 24)))))

(def test-numbers
  '(7 4 9 5 11 17 23 2 0 14 21 24 10 16 13 6 15 25 12 22 18 20 8 19 3 26 1))

(def test-board-rows-1
  [[22 13 17 11  0]
   [8  2 23  4 24]
   [21  9 14 16  7]
   [6 10  3 18  5]
   [1 12 20 15 19]])

(def test-board-rows-2
  [[3 15  0  2 22]
   [9 18 13 17  5]
   [19  8  7 25 23]
   [20 11 10 24  4]
   [14 21 16 12  6]])

(def test-board-rows-3
  [[14 21 17 24  4]
   [10 16 15  9 19]
   [18  8 23 26 20]
   [22 11 13  6  5]
   [2  0 12  3  7]])

(deftest board-init-test
  (testing "Initializes a bingo board from a vector of 5 rows"
    (is (= [[22 :clear] [13 :clear] [17 :clear] [11 :clear] [0 :clear]
            [8 :clear] [2 :clear] [23 :clear] [4 :clear] [24 :clear]
            [21 :clear] [9 :clear] [14 :clear] [16 :clear] [7 :clear]
            [6 :clear] [10 :clear] [3 :clear] [18 :clear] [5 :clear]
            [1 :clear] [12 :clear] [20 :clear] [15 :clear] [19 :clear]]
           (board-init test-board-rows-1)))
    (is (= [[3 :clear] [15 :clear] [0 :clear] [2 :clear] [22 :clear]
            [9 :clear] [18 :clear] [13 :clear] [17 :clear] [5 :clear]
            [19 :clear] [8 :clear] [7 :clear] [25 :clear] [23 :clear]
            [20 :clear] [11 :clear] [10 :clear] [24 :clear] [4 :clear]
            [14 :clear] [21 :clear] [16 :clear] [12 :clear] [6 :clear]]
           (board-init test-board-rows-2)))
    (is (= [[14 :clear] [21 :clear] [17 :clear] [24 :clear] [4 :clear]
            [10 :clear] [16 :clear] [15 :clear] [9 :clear] [19 :clear]
            [18 :clear] [8 :clear] [23 :clear] [26 :clear] [20 :clear]
            [22 :clear] [11 :clear] [13 :clear] [6 :clear] [5 :clear]
            [2 :clear] [0 :clear] [12 :clear] [3 :clear] [7 :clear]]
           (board-init test-board-rows-3)))))

(def test-board-1 (board-init test-board-rows-1))
(def test-board-2 (board-init test-board-rows-2))
(def test-board-3 (board-init test-board-rows-3))
(def test-boards (list test-board-1 test-board-2 test-board-3))

(deftest board-mark-test
  (testing "Marks number on board if number is present"
    (is (= [[22 :clear] [13 :clear] [17 :clear] [11 :clear] [0 :clear]
            [8 :clear] [2 :clear] [23 :clear] [4 :clear] [24 :clear]
            [21 :clear] [9 :clear] [14 :clear] [16 :clear] [7 :clear]
            [6 :clear] [10 :clear] [3 :clear] [18 :clear] [5 :clear]
            [1 :clear] [12 :clear] [20 :clear] [15 :clear] [19 :clear]]
           (board-mark test-board-1 65)))
    (is (= [[22 :clear] [13 :clear]  [17 :clear] [11 :clear] [0 :clear]
            [8 :clear] [2 :clear]  [23 :clear] [4 :clear] [24 :clear]
            [21 :clear] [9 :marked] [14 :clear] [16 :clear] [7 :clear]
            [6 :clear] [10 :clear]  [3 :clear] [18 :clear] [5 :clear]
            [1 :clear] [12 :clear]  [20 :clear] [15 :clear] [19 :clear]]
           (board-mark test-board-1 9)))
    (is (= [[22 :clear]  [13 :clear] [17 :clear] [11 :clear] [0 :clear]
            [8 :clear]  [2 :clear] [23 :clear] [4 :clear] [24 :clear]
            [21 :marked] [9 :clear] [14 :clear] [16 :clear] [7 :clear]
            [6 :clear]  [10 :clear] [3 :clear] [18 :clear] [5 :clear]
            [1 :clear]  [12 :clear] [20 :clear] [15 :clear] [19 :clear]]
           (board-mark test-board-1 21)))
    (is (= [[3 :clear] [15 :clear] [0 :clear] [2 :clear]  [22 :clear]
            [9 :clear] [18 :clear] [13 :clear] [17 :marked] [5 :clear]
            [19 :clear] [8 :clear] [7 :clear] [25 :clear]  [23 :clear]
            [20 :clear] [11 :clear] [10 :clear] [24 :clear]  [4 :clear]
            [14 :clear] [21 :clear] [16 :clear] [12 :clear]  [6 :clear]]
           (board-mark test-board-2 17)))
    (is (= [[22 :clear]  [13 :clear]  [17 :marked] [11 :marked] [0 :marked]
            [8 :clear]  [2 :marked] [23 :marked] [4 :marked] [24 :marked]
            [21 :marked] [9 :marked] [14 :marked] [16 :clear]  [7 :marked]
            [6 :clear]  [10 :clear]  [3 :clear]  [18 :clear]  [5 :marked]
            [1 :clear]  [12 :clear]  [20 :clear]  [15 :clear]  [19 :clear]]
           (board-mark-all test-board-1 '(7 4 9 5 11 17 23 2 0 14 21 24))))
    (is (= [[3 :clear]  [15 :clear]  [0 :marked] [2 :marked]  [22 :clear]
            [9 :marked] [18 :clear]  [13 :clear]  [17 :marked]  [5 :marked]
            [19 :clear]  [8 :clear]  [7 :marked] [25 :clear]   [23 :marked]
            [20 :clear]  [11 :marked] [10 :clear]  [24 :marked]  [4 :marked]
            [14 :marked] [21 :marked] [16 :clear]  [12 :clear]   [6 :clear]]
           (board-mark-all test-board-2 '(7 4 9 5 11 17 23 2 0 14 21 24))))
    (is (= [[14 :marked] [21 :marked] [17 :marked] [24 :marked] [4 :marked]
            [10 :clear]  [16 :clear]  [15 :clear]  [9 :marked] [19 :clear]
            [18 :clear]  [8 :clear]  [23 :marked] [26 :clear]  [20 :clear]
            [22 :clear]  [11 :marked] [13 :clear]  [6 :clear]  [5 :marked]
            [2 :marked] [0 :marked] [12 :clear]  [3 :clear]  [7 :marked]]
           (board-mark-all test-board-3 '(7 4 9 5 11 17 23 2 0 14 21 24))))))

(deftest board-rows-test
  (testing "Gets the rows on a board"
    (is (= ['([22 :clear] [13 :clear] [17 :clear] [11 :clear] [0 :clear])
            '([8 :clear] [2 :clear] [23 :clear] [4 :clear] [24 :clear])
            '([21 :clear] [9 :clear] [14 :clear] [16 :clear] [7 :clear])
            '([6 :clear] [10 :clear] [3 :clear] [18 :clear] [5 :clear])
            '([1 :clear] [12 :clear] [20 :clear] [15 :clear] [19 :clear])]
           (board-rows test-board-1)))
    (is (= ['([22 :clear] [13 :clear] [17 :clear] [11 :clear] [0 :clear])
            '([8 :clear] [2 :clear] [23 :clear] [4 :clear] [24 :clear])
            '([21 :clear] [9 :clear] [14 :clear] [16 :clear] [7 :clear])
            '([6 :clear] [10 :clear] [3 :clear] [18 :clear] [5 :clear])
            '([1 :clear] [12 :clear] [20 :clear] [15 :clear] [19 :clear])]
           (board-rows (board-mark test-board-1 65))))
    (is (= ['([22 :clear] [13 :clear]  [17 :clear] [11 :clear] [0 :clear])
            '([8 :clear] [2 :clear]  [23 :clear] [4 :clear] [24 :clear])
            '([21 :clear] [9 :marked] [14 :clear] [16 :clear] [7 :clear])
            '([6 :clear] [10 :clear]  [3 :clear] [18 :clear] [5 :clear])
            '([1 :clear] [12 :clear]  [20 :clear] [15 :clear] [19 :clear])]
           (board-rows (board-mark test-board-1 9))))))

(deftest board-cols-test
  (testing "Gets the columns on a board"
    (is (= ['([22 :clear] [8 :clear] [21 :clear] [6 :clear] [1 :clear])
            '([13 :clear] [2 :clear] [9 :clear] [10 :clear] [12 :clear])
            '([17 :clear] [23 :clear] [14 :clear] [3 :clear] [20 :clear])
            '([11 :clear] [4 :clear] [16 :clear] [18 :clear] [15 :clear])
            '([0 :clear] [24 :clear] [7 :clear] [5 :clear] [19 :clear])]
           (board-cols test-board-1)))))

(deftest bingo?-test
  (testing "Marks number on board if number is present"
    (testing "Empty boards never bingo"
      (is (not (bingo? test-board-1)))
      (is (not (bingo? test-board-2)))
      (is (not (bingo? test-board-3))))
    (testing "Advent of Code sample game"
      (is (not (bingo? (board-mark-all test-board-1 '(7 4 9 5 11 17 23 2 0 14 21 24)))))
      (is (not (bingo? (board-mark-all test-board-2 '(7 4 9 5 11 17 23 2 0 14 21 24)))))
      (is (bingo? (board-mark-all test-board-3 '(7 4 9 5 11 17 23 2 0 14 21 24)))))
    (testing "Row and column bingos for Advent of Code board 1"
      (is (bingo? (board-mark-all test-board-1 '(22 13 17 11 0))))
      (is (bingo? (board-mark-all test-board-1 '(22 8 21 6 1)))))
    (testing "Row and column bingos for Advent of Code board 2"
      (is (bingo? (board-mark-all test-board-2 '(9 18 13 17 5))))
      (is (bingo? (board-mark-all test-board-2 '(15 18 8 11 21)))))
    (testing "Row and column bingos for Advent of Code board 3"
      (is (bingo? (board-mark-all test-board-3 '(2 0 12 3 7))))
      (is (bingo? (board-mark-all test-board-3 '(4 19 20 5 7)))))))

(deftest game-next-test
  (testing "Draws a number and marks all board cells containing that number"
    (is (= {:winner         nil
            :numbers-called (reverse (take 1 test-numbers))
            :boards         (list
                             (board-mark test-board-1 7)
                             (board-mark test-board-2 7)
                             (board-mark test-board-3 7))
            :numbers        (drop 1 test-numbers)}
           (game-next (game-init test-boards test-numbers))))))

(deftest play-game-test
  (testing "Plays a full bingo game"
    (let [test-numbers-drawn      '(7 4 9 5 11 17 23 2 0 14 21 24)
          expected-numbers-called (reverse test-numbers-drawn)
          expected-final-board-1  (board-mark-all test-board-1 test-numbers-drawn)
          expected-final-board-2  (board-mark-all test-board-2 test-numbers-drawn)
          expected-final-board-3  (board-mark-all test-board-3 test-numbers-drawn)
          expected-boards         (list expected-final-board-1 expected-final-board-2 expected-final-board-3)
          expected-numbers        (drop (count test-numbers-drawn) test-numbers)]
      (is (= {:winner         expected-final-board-3
              :numbers-called expected-numbers-called
              :boards         expected-boards
              :numbers        expected-numbers}
             (play-game test-boards test-numbers))))))

(deftest lines->domain-test
  (testing "Loads inputs and translates them to the domain"
    (is (= {:numbers test-numbers, :boards test-boards}
           (lines->domain test-input-lines)))))

(deftest game-score-test
  (testing "Plays a full bingo game and calculates the score"
    (is (= 4512
           (game-score (play-game test-boards test-numbers))))))
