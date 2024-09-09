# frozen_string_literal: true

class WrappingPaperCalculator
  def calculate(dimensions)
    length, width, height = parse(dimensions)
    calc_exact(length, width, height) + calc_slack(length, width, height)
  end

private

  def parse(dimensions)
    dimensions.split(/x/).map(&:to_i)
  end

  def calc_exact(length, width, height)
    2 * ((length * width) + (width * height) + (height * length))
  end

  def calc_slack(length, width, height)
    [length * width, width * height, height * length].min
  end
end
