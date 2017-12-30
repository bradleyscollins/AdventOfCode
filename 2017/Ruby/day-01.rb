# Puzzle: http://adventofcode.com/2017/day/1
# Input:  http://adventofcode.com/2017/day/1/input

# --- Day 1: Inverse Captcha ---
# 
# The night before Christmas, one of Santa's Elves calls you in a panic.
# "The printer's broken! We can't print the Naughty or Nice List!" By the time
# you make it to sub-basement 17, there are only a few minutes until midnight.
# "We have a big problem," she says; "there must be almost fifty bugs in this
# system, but nothing else can print The List. Stand in this square, quick!
# There's no time to explain; if you can convince them to pay you in stars,
# you'll be able to--" She pulls a lever and the world goes blurry.
# 
# When your eyes can focus again, everything seems a lot more pixelated than
# before. She must have sent you inside the computer! You check the system
# clock: 25 milliseconds until midnight. With that much time, you should be able
# to collect all fifty stars by December 25th.
# 
# Collect stars by solving puzzles. Two puzzles will be made available on each
# day millisecond in the advent calendar; the second puzzle is unlocked when you
# complete the first. Each puzzle grants one star. Good luck!
# 
# You're standing in a room with "digitization quarantine" written in LEDs along
# one wall. The only door is locked, but it includes a small interface.
# "Restricted Area - Strictly No Digitized Users Allowed."
# 
# It goes on to explain that you may only leave by solving a captcha to prove
# you're not a human. Apparently, you only get one millisecond to solve the
# captcha: too fast for a normal human, but it feels like hours to you.
# 
# The captcha requires you to review a sequence of digits (your puzzle input)
# and find the sum of all digits that match the next digit in the list. The list
# is circular, so the digit after the last digit is the first digit in the list.
# 
# For example:
# 
#  - 1122 produces a sum of 3 (1 + 2) because the first digit (1) matches the
#    second digit and the third digit (2) matches the fourth digit.
#  - 1111 produces 4 because each digit (all 1) matches the next.
#  - 1234 produces 0 because no digit matches the next.
#  - 91212129 produces 9 because the only digit that matches the next one is the
#    last digit, 9.
#
# What is the solution to your captcha?
# 
# Your puzzle answer was 1044.
# 
# The first half of this puzzle is complete! It provides one gold star: *
#
#
# --- Part Two ---
# 
# You notice a progress bar that jumps to 50% completion. Apparently, the door
# isn't yet satisfied, but it did emit a star as encouragement. The instructions
# change:
# 
# Now, instead of considering the next digit, it wants you to consider the digit
# halfway around the circular list. That is, if your list contains 10 items,
# only include a digit in your sum if the digit 10/2 = 5 steps forward matches
# it. Fortunately, your list has an even number of elements.
# 
# For example:
# 
#  - 1212 produces 6: the list contains 4 items, and all four digits match the
#    digit 2 items ahead.
#  - 1221 produces 0, because every comparison is between a 1 and a 2.
#  - 123425 produces 4, because both 2s match each other, but no other digit has
#    a match.
#  - 123123 produces 12.
#  - 12131415 produces 4.
#
# What is the solution to your new captcha?
#
# Your puzzle answer was 1054.
#
# Both parts of this puzzle are complete! They provide two gold stars: **

input = '111831362354551173134957758417849716877188716338227121869992652972154651632296676464285261171625892888598738721925357479249486886375279741651224686642647267979445939836673253446489428761486828844713816198414852769942459766921928735591892723619845983117283575762694758223956262583556675379533479458964152461973321432768858165818549484229241869657725166769662249574889435227698271439423511175653875622976121749344756734658248245212273242115488961818719828258936653236351924292251821352389471971641957941593141159982696396228218461855752555358856127582128823657548151545741663495182446281491763249374581774426225822474112338745629194213976328762985884127324443984163571711941113986826168921187567861288268744663142867866165546795621466134333541274633769865956692539151971953651886381195877638919355216642731848659649263217258599456646635412623461138792945854536154976732167439355548965778313264824237176152196614333748919711422188148687299757751955297978137561935963366682742334867854892581388263132968999722366495346854828316842352829827989419393594846893842746149235681921951476132585199265366836257322121681471877187847219712325933714149151568922456111149524629995933156924418468567649494728828858254296824372929211977446729691143995333874752448315632185286348657293395339475256796591968717487615896959976413637422536563273537972841783386358764761364989261322293887361558128521915542454126546182855197637753115352541578972298715522386683914777967729562229395936593272269661295295223113186683594678533511783187422193626234573849881185849626389774394351115527451886962844431947188429195191724662982411619815811652741733744864411666766133951954595344837179635668177845937578575117168875754181523584442699384167111317875138179567939174589917894597492816476662186746837552978671142265114426813792549412632291424594239391853358914643327549192165466628737614581458189732579814919468795493415762517372227862614224911844744711698557324454211123571327224554259626961741919243229688684838813912553397698937237114287944446722919198743189848428399356842626198635297851274879128322358195585284984366515428245928111112613638341345371'

def sum_matching_pairs(digits, offset)
  length = digits.length
  pairs = digits.each_with_index.map do |x,i|
    counterpart_index = (i + offset) % length
    [x, digits[counterpart_index]]
  end
  matching_pairs = pairs.find_all { |x,y| x == y }
  matching_pairs.map { |pair| pair.first }.sum
end

digits = input.chars.map(&:to_i).to_a

puts "The sum for part 1 is #{sum_matching_pairs(digits, 1)}"
puts "The sum for part 2 is #{sum_matching_pairs(digits, digits.length / 2)}"
