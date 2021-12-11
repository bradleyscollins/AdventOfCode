(ns day-10.core-test
  (:require [clojure.test :refer :all]
            [day-10.core :refer :all]))

(def test-input
  ["[({(<(())[]>[[{[]{<()<>>"
   "[(()[<>])]({[<{<<[]>>("
   "{([(<{}[<>[]}>{[]{[(<()>"
   "(((({<>}<{<{<>}{[]{[]{}"
   "[[<[([]))<([[{}[[()]]]"
   "[{[{({}]{}}([{[{{{}}([]"
   "{<[[]]>}<{[{[{[]{()[[[]"
   "[<(<(<(<{}))><([]([]()"
   "<{([([[(<>()){}]>(<<{{"
   "<{([{{}}[<[[[<>{}]]]>[]]"])

(deftest check-syntax-test
  (testing "Checks the syntax of a line of navigation subsystem syntax"
    (is (= [:corrupted { :expected \], :found \} }]
           (check-syntax "{([(<{}[<>[]}>{[]{[(<()>")))
    (is (= [:corrupted { :expected \], :found \) }]
           (check-syntax "[[<[([]))<([[{}[[()]]]")))
    (is (= [:corrupted { :expected \), :found \] }]
           (check-syntax "[{[{({}]{}}([{[{{{}}([]")))
    (is (= [:corrupted { :expected \>, :found \) }]
           (check-syntax "[<(<(<(<{}))><([]([]()")))
    (is (= [:corrupted { :expected \], :found \> }]
           (check-syntax "<{([([[(<>()){}]>(<<{{")))
    (is (= [:incomplete {:expected "}}]])})]"}]
           (check-syntax "[({(<(())[]>[[{[]{<()<>>")))))

(deftest score-results-test
  (testing "Tallies up the total results of all the inputs"
    (let [results (map check-syntax test-input)]
      (testing "Sum of scores for corrupted lines"
        (let [corrupted (filter result-corrupted? results)]
          (is (= 26397
                 (sum-corrupted-line-scores corrupted)))))

      (testing "Median of (sorted) scores for incomplete lines"
        (is (= 288957
               (calc-median-incomplete-line-score results)))))))