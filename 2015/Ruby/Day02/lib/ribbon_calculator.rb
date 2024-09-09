# frozen_string_literal: true

class RibbonCalculator
  def calculate(dimensions)
    length, width, height = parse(dimensions)
    calc_ribbon_length(length, width, height) + calc_bow_length(length, width, height)
  end

private

  def parse(dimensions)
    dimensions.split(/x/).map(&:to_i)
  end

  def calc_ribbon_length(length, width, height)
    [
      calc_perimeter(length, width),
      calc_perimeter(width, height),
      calc_perimeter(height, length),
    ].min
  end

  def calc_perimeter(x, y)
    2 * (x + y)
  end

  def calc_bow_length(length, width, height)
    length * width * height
  end
end
