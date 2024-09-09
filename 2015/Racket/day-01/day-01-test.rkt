#lang racket

(require rackunit
         "day-01.rkt")

(check-equal? (which-floor "(())")     0)
(check-equal? (which-floor "()()")     0)
(check-equal? (which-floor "(((")      3)
(check-equal? (which-floor "(()(()(")  3)
(check-equal? (which-floor "))(((((")  3)
(check-equal? (which-floor "())")     -1)
(check-equal? (which-floor "))(")     -1)
(check-equal? (which-floor ")))")     -3)
(check-equal? (which-floor ")())())") -3)

(check-equal? (which-floor-alt "(())")     0)
(check-equal? (which-floor-alt "()()")     0)
(check-equal? (which-floor-alt "(((")      3)
(check-equal? (which-floor-alt "(()(()(")  3)
(check-equal? (which-floor-alt "))(((((")  3)
(check-equal? (which-floor-alt "())")     -1)
(check-equal? (which-floor-alt "))(")     -1)
(check-equal? (which-floor-alt ")))")     -3)
(check-equal? (which-floor-alt ")())())") -3)
