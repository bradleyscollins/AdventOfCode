# frozen_string_literal: true

require_relative 'partner_visited_house_calculator'
require_relative 'visited_house_calculator'

class App
  def run
    moves = ARGF.read.strip

    visit_calc = VisitedHouseCalculator.new([0, 0])
    houses_visited = visit_calc.calculate(moves)
    puts "How many houses receive at least one present? #{houses_visited}"

    partner_visit_calc = PartnerVisitedHouseCalculator.new(
      partners: 2,
      initial_position: [0, 0])
    houses_visited_by_santa_and_robo = partner_visit_calc.calculate(moves)
    puts "How many houses receive at least one present from either Santa or Robo-Santa? #{houses_visited_by_santa_and_robo}"
  end
end

App.new.run
