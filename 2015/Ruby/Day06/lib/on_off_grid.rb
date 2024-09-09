# frozen_string_literal: true

class OnOffGrid
  attr_reader :lights

  def initialize
    @lights = Array.new(1000) { Array.new(1000, false) }
  end

  def apply(instruction)
    points = instruction.range

    case instruction.command
    when :on
      points.each { |x,y| @lights[x][y] = true }
    when :off
      points.each { |x,y| @lights[x][y] = false }
    else
      points.each { |x,y| @lights[x][y] = !@lights[x][y] }
    end
  end

  def lights_lit
    @lights.flatten.count { _1 }
  end
end
