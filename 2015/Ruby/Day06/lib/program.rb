# frozen_string_literal: true

require_relative 'instruction'
require_relative 'grid'

class App
  def run
    instructions = ARGF.readlines.map { Instruction.parse(_1.strip) }
    grid = Grid.new

    instructions.each { grid.apply _1 }

    puts "How many lights are lit? #{grid.lights_lit}"
  end
end

App.new.run
