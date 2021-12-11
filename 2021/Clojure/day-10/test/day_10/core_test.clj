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
           (check-syntax "<{([([[(<>()){}]>(<<{{")))))

(deftest score-results-test
  (testing "Tallies up the total results of all the inputs"
    (is (= 26397
           (score-results (map check-syntax test-input))))))