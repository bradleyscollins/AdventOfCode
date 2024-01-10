# frozen_string_literal: true

require_relative 'instruction'
require_relative 'brightness_grid'
require_relative 'on_off_grid'

class App
  def run
    instructions = ARGF.readlines.map { Instruction.parse(_1.strip) }

    on_off_grid = OnOffGrid.new
    instructions.each { on_off_grid.apply _1 }
    puts "How many lights are lit? #{on_off_grid.lights_lit}"

    brightness_grid = BrightnessGrid.new
    instructions.each { brightness_grid.apply _1 }
    puts "What is the grid's total brightness? #{brightness_grid.total_brightness}"
  end
end

App.new.run
