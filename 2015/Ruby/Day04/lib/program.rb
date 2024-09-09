# frozen_string_literal: true

require_relative 'advent_coin'

class App
  def run
    advent_coin = AdventCoin.new
    puts "Lowest positive number producing an MD5 hash with ..."
    puts "... at least 5 leading zeros? #{advent_coin.find_number('iwrupvqb', 5)}"
    puts "... at least 6 leading zeros? #{advent_coin.find_number('iwrupvqb', 6)}"
  end
end

App.new.run
