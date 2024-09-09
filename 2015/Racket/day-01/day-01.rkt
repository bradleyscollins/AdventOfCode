#lang racket

(define (char->movement char)
  (if (equal? char #\()
      1
      -1))

(define (chars->movements chars)
  (map char->movement chars))

(define (sum lst)
  (apply + lst))

(define (which-floor instructions)
  (let* ([chars     (string->list instructions)]
         [movements (map (λ (c) (if (equal? c #\() 1 -1)) chars)])
    (sum movements)))

(define which-floor-alt (compose sum chars->movements string->list))

(provide which-floor
         which-floor-alt)
