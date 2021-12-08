(ns day-08.core-test
  (:require [clojure.test :refer :all]
            [day-08.core :refer :all]))

(def test-input
  (list
   "be cfbegad cbdgef fgaecd cgeb fdcge agebfd fecdb fabcd edb | fdgacbe cefdb cefbgd gcbe"
   "edbfga begcd cbg gc gcadebf fbgde acbgfd abcde gfcbed gfec | fcgedb cgb dgebacf gc"
   "fgaebd cg bdaec gdafb agbcfd gdcbef bgcad gfac gcb cdgabef | cg cg fdcagb cbg"
   "fbegcd cbd adcefb dageb afcb bc aefdc ecdab fgdeca fcdbega | efabcd cedba gadfec cb"
   "aecbfdg fbg gf bafeg dbefa fcge gcbea fcaegb dgceab fcbdga | gecf egdcabf bgf bfgea"
   "fgeab ca afcebg bdacfeg cfaedg gcfdb baec bfadeg bafgc acf | gebdcfa ecba ca fadegcb"
   "dbcfg fgd bdegcaf fgec aegbdf ecdfab fbedc dacgb gdcebf gf | cefg dcbef fcge gbcadfe"
   "bdfegc cbegaf gecbf dfcage bdacg ed bedf ced adcbefg gebcd | ed bcgafe cdgba cbgef"
   "egadfb cdbfeg cegd fecab cgb gbdefca cg fgcdab egfdb bfceg | gbdfcae bgc cg cgb"
   "gcafb gcf dcaebfg ecagb gf abcdeg gaef cafbge fdbac fegbdc | fgae cfgab fg bagce"))

(deftest signal-patterns->pattern-map-test
  (testing "Converts a list of signal patterns into a pattern map"
    (is (= {"be" 1, "edb" 7, "cgeb" 4, "cfbegad" 8}
           (signal-patterns->pattern-map ["be" "cfbegad" "cbdgef" "fgaecd" "cgeb" "fdcge" "agebfd" "fecdb" "fabcd" "edb"])))
    (is (= {"gc" 1, "cbg" 7, "gfec" 4, "gcadebf" 8}
           (signal-patterns->pattern-map ["edbfga" "begcd" "cbg" "gc" "gcadebf" "fbgde" "acbgfd" "abcde" "gfcbed" "gfec"])))
    (is (= {"cg" 1, "gcb" 7 "gfac" 4, "cdgabef" 8}
           (signal-patterns->pattern-map ["fgaebd" "cg" "bdaec" "gdafb" "agbcfd" "gdcbef" "bgcad" "gfac" "gcb" "cdgabef"])))
    (is (= {"bc" 1, "cbd" 7, "afcb" 4, "fcdbega" 8}
           (signal-patterns->pattern-map ["fbegcd" "cbd" "adcefb" "dageb" "afcb" "bc" "aefdc" "ecdab" "fgdeca" "fcdbega"])))
    (is (= {"gf" 1, "fbg" 7, "fcge" 4, "aecbfdg" 8}
           (signal-patterns->pattern-map ["aecbfdg" "fbg" "gf" "bafeg" "dbefa" "fcge" "gcbea" "fcaegb" "dgceab" "fcbdga"])))
    (is (= {"ca" 1, "acf" 7, "baec" 4, "bdacfeg" 8}
           (signal-patterns->pattern-map ["fgeab" "ca" "afcebg" "bdacfeg" "cfaedg" "gcfdb" "baec" "bfadeg" "bafgc" "acf"])))
    (is (= {"gf" 1, "fgd" 7, "fgec" 4, "bdegcaf" 8}
           (signal-patterns->pattern-map ["dbcfg" "fgd" "bdegcaf" "fgec" "aegbdf" "ecdfab" "fbedc" "dacgb" "gdcebf" "gf"])))
    (is (= {"ed" 1, "ced" 7, "bedf" 4, "adcbefg" 8}
           (signal-patterns->pattern-map ["bdfegc" "cbegaf" "gecbf" "dfcage" "bdacg" "ed" "bedf" "ced" "adcbefg" "gebcd"])))
    (is (= {"cg" 1, "cgb" 7, "cegd" 4, "gbdefca" 8}
           (signal-patterns->pattern-map ["egadfb" "cdbfeg" "cegd" "fecab" "cgb" "gbdefca" "cg" "fgcdab" "egfdb" "bfceg"])))
    (is (= {"gf" 1, "gcf" 7, "gaef" 4, "dcaebfg" 8}
           (signal-patterns->pattern-map ["gcafb" "gcf" "dcaebfg" "ecagb" "gf" "abcdeg" "gaef" "cafbge" "fdbac" "fegbdc"])))))

(deftest str->signal-pattern-map-test
  (testing "Converts a string into a pattern map"
    ;; (is (= {"be" 1, "edb" 7, "cgeb" 4, "cfbegad" 8}
    ;;        (signal-patterns->pattern-map ["be" "cfbegad" "cbdgef" "fgaecd" "cgeb" "fdcge" "agebfd" "fecdb" "fabcd" "edb"])))
    ;; (is (= {"gc" 1, "cbg" 7, "gfec" 4, "gcadebf" 8}
    ;;        (signal-patterns->pattern-map ["edbfga" "begcd" "cbg" "gc" "gcadebf" "fbgde" "acbgfd" "abcde" "gfcbed" "gfec"])))
    ;; (is (= {"cg" 1, "gcb" 7 "gfac" 4, "cdgabef" 8}
    ;;        (signal-patterns->pattern-map ["fgaebd" "cg" "bdaec" "gdafb" "agbcfd" "gdcbef" "bgcad" "gfac" "gcb" "cdgabef"])))
    ;; (is (= {"bc" 1, "cbd" 7, "afcb" 4, "fcdbega" 8}
    ;;        (signal-patterns->pattern-map ["fbegcd" "cbd" "adcefb" "dageb" "afcb" "bc" "aefdc" "ecdab" "fgdeca" "fcdbega"])))
    ;; (is (= {"gf" 1, "fbg" 7, "fcge" 4, "aecbfdg" 8}
    ;;        (signal-patterns->pattern-map ["aecbfdg" "fbg" "gf" "bafeg" "dbefa" "fcge" "gcbea" "fcaegb" "dgceab" "fcbdga"])))
    ;; (is (= {"ca" 1, "acf" 7, "baec" 4, "bdacfeg" 8}
    ;;        (signal-patterns->pattern-map ["fgeab" "ca" "afcebg" "bdacfeg" "cfaedg" "gcfdb" "baec" "bfadeg" "bafgc" "acf"])))
    ;; (is (= {"gf" 1, "fgd" 7, "fgec" 4, "bdegcaf" 8}
    ;;        (signal-patterns->pattern-map ["dbcfg" "fgd" "bdegcaf" "fgec" "aegbdf" "ecdfab" "fbedc" "dacgb" "gdcebf" "gf"])))
    ;; (is (= {"ed" 1, "ced" 7, "bedf" 4, "adcbefg" 8}
    ;;        (signal-patterns->pattern-map ["bdfegc" "cbegaf" "gecbf" "dfcage" "bdacg" "ed" "bedf" "ced" "adcbefg" "gebcd"])))
    ;; (is (= {"cg" 1, "cgb" 7, "cegd" 4, "gbdefca" 8}
    ;;        (signal-patterns->pattern-map ["egadfb" "cdbfeg" "cegd" "fecab" "cgb" "gbdefca" "cg" "fgcdab" "egfdb" "bfceg"])))
    (is (= {"gf" 1, "gcf" 7, "gaef" 4, "dcaebfg" 8}
           (signal-patterns->pattern-map "gcafb gcf dcaebfg ecagb gf abcdeg gaef cafbge fdbac fegbdc")))))

(deftest decode-display-test
  (testing "Converts a list of signal patterns into a pattern map"
    (is (= [8 4]
           (decode-display {"be" 1, "edb" 7, "cgeb" 4, "cfbegad" 8}
                           ["fdgacbe" "cefdb" "cefbgd" "gcbe"])))
    (is (= [7 8 1]
           (decode-display {"gc" 1, "cbg" 7, "gfec" 4, "gcadebf" 8}
                           ["fcgedb" "cgb" "dgebacf" "gc"])))
    (is (= [1 1 7]
           (decode-display {"cg" 1, "gcb" 7 "gfac" 4, "cdgabef" 8}
                           ["cg" "cg" "fdcagb" "cbg"])))
    (is (= [1]
           (decode-display {"bc" 1, "cbd" 7, "afcb" 4, "fcdbega" 8}
                           ["efabcd" "cedba" "gadfec" "cb"])))
    (is (= [4 8 7]
           (decode-display {"gf" 1, "fbg" 7, "fcge" 4, "aecbfdg" 8}
                           ["gecf" "egdcabf" "bgf" "bfgea"])))
    (is (= [8 4 1 8]
           (decode-display {"ca" 1, "acf" 7, "baec" 4, "bdacfeg" 8}
                           ["gebdcfa" "ecba" "ca" "fadegcb"])))
    (is (= [4 4 8]
           (decode-display {"gf" 1, "fgd" 7, "fgec" 4, "bdegcaf" 8}
                           ["cefg" "dcbef" "fcge" "gbcadfe"])))
    (is (= [1]
           (decode-display {"ed" 1, "ced" 7, "bedf" 4, "adcbefg" 8}
                           ["ed" "bcgafe" "cdgba" "cbgef"])))
    (is (= [8 7 1 7]
           (decode-display {"cg" 1, "cgb" 7, "cegd" 4, "gbdefca" 8}
                           ["gbdfcae" "bgc" "cg" "cgb"])))
    (is (= [4 1]
           (decode-display {"gf" 1, "gcf" 7, "gaef" 4, "dcaebfg" 8}
                           ["fgae" "cfgab" "fg" "bagce"])))))

(deftest str->signal-pattern-map-test
  (testing "Converts a list of signal patterns into a pattern map"
    (is (= {"be" 1, "edb" 7, "cgeb" 4, "cfbegad" 8}
           (str->signal-pattern-map "be cfbegad cbdgef fgaecd cgeb fdcge agebfd fecdb fabcd edb")))))

(deftest str->domain-test
  (testing "Converts a list of signal patterns into a pattern map"
    (is (= [{"be" 1, "edb" 7, "cgeb" 4, "cfbegad" 8}
            ["fdgacbe" "cefdb" "cefbgd" "gcbe"]]
           (str->domain "be cfbegad cbdgef fgaecd cgeb fdcge agebfd fecdb fabcd edb | fdgacbe cefdb cefbgd gcbe")))))
