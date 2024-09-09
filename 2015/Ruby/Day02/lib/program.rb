# frozen_string_literal: true

require_relative 'ribbon_calculator'
require_relative 'wrapping_paper_calculator'

class App
  def run
    dimensions = ARGF.readlines.map(&:chomp)

    paper_calc = WrappingPaperCalculator.new
    total_wrapping_paper_sqft = dimensions.map { paper_calc.calculate _1 }.sum
    puts "Total square feet of wrapping paper needed: #{total_wrapping_paper_sqft}"

    ribbon_calc = RibbonCalculator.new
    total_ribbon_ft = dimensions.map { ribbon_calc.calculate _1 }.sum
    puts "Total length (feet) of ribbon needed: #{total_ribbon_ft}"
  end
end

App.new.run
