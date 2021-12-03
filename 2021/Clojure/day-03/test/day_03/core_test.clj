(ns day-03.core-test
  (:require [clojure.test :refer :all]
            [day-03.core :refer :all]))

(deftest test-digitize
  (testing "digitize splits string representations of numbers into individual digits"
    (is (= '(0 0 1 0 0)               (digitize "00100")))
    (is (= '(1 1 1 1 0)               (digitize "11110")))
    (is (= '(1 0 1 1 0)               (digitize "10110")))
    (is (= '(0 0 1 0 0 1 1 0 0 1 0 1) (digitize "001001100101")))
    (is (= '(0 1 0 1 0 0 0 1 1 1 0 0) (digitize "010100011100")))))

(def test-inputs '("00100"
                   "11110"
                   "10110"
                   "10111"
                   "10101"
                   "01111"
                   "00111"
                   "11100"
                   "10000"
                   "11001"
                   "00010"
                   "01010"))

(deftest test-columnize
  (testing "Turns a list of inputs into columns"
    (is (= (list '(0 1 1 1 1 0 0 1 1 1 0 0)
                 '(0 1 0 0 0 1 0 1 0 1 0 1)
                 '(1 1 1 1 1 1 1 1 0 0 0 0)
                 '(0 1 1 1 0 1 1 0 0 0 1 1)
                 '(0 0 0 1 1 1 1 0 0 1 0 0))
           (columnize test-inputs)))))

(deftest test-histograms
  (testing "Converts columns into histograms"
    (is (= (list {0 5, 1 7}
                 {0 7, 1 5}
                 {0 4, 1 8}
                 {0 5, 1 7}
                 {0 7, 1 5})
           (histograms (columnize test-inputs))))))

(deftest test-key-of-max-value
  (testing "Gets the key of the map entry with the greatest value"
    (is (= 1 (key-of-max-value {0 5, 1 7})))
    (is (= 0 (key-of-max-value {0 7, 1 5})))
    (is (= 1 (key-of-max-value {0 4, 1 8})))
    (is (= 1 (key-of-max-value {0 5, 1 7})))
    (is (= 0 (key-of-max-value {0 9, 1 3})))))

(deftest test-key-of-min-value
  (testing "Gets the key of the map entry with the least value"
    (is (= 0 (key-of-min-value {0 5, 1 7})))
    (is (= 1 (key-of-min-value {0 7, 1 5})))
    (is (= 0 (key-of-min-value {0 4, 1 8})))
    (is (= 0 (key-of-min-value {0 5, 1 7})))
    (is (= 1 (key-of-min-value {0 9, 1 3})))))

(deftest test-calc-gamma-rate
  (testing "Converts column histograms into a gamma rate"
    (is (= 22
           (calc-gamma-rate (histograms (columnize test-inputs)))))))

(deftest test-calc-epsilon-rate
  (testing "Converts column histograms into an epsilon rate"
    (is (= 9
           (calc-epsilon-rate (histograms (columnize test-inputs)))))))
