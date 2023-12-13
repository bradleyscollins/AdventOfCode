# frozen_string_literal: true

require_relative 'visited_house_calculator'

class App
  def run
    moves = ARGF.read.strip

    visit_calc = VisitedHouseCalculator.new([0, 0])
    houses_visited = visit_calc.calculate(moves)
    puts "How many houses receive at least one present? #{houses_visited}"
  end
end

App.new.run
