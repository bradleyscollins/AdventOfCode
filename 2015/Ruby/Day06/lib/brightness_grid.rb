# frozen_string_literal: true

class BrightnessGrid
  attr_reader :lights

  def initialize
    @lights = Array.new(1000) { Array.new(1000, 0) }
  end

  def apply(instruction)
    points = instruction.range

    case instruction.command
    when :on
      points.each { |x,y| @lights[x][y] += 1 }
    when :off
      points.each { |x,y| @lights[x][y] = (@lights[x][y] - 1).clamp(0..) }
    else
      points.each { |x,y| @lights[x][y] += 2 }
    end
  end

  def total_brightness
    @lights.flatten.sum
  end
end
