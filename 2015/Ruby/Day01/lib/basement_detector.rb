# frozen_string_literal: true

class BasementDetector
  def find_basement(code)
    steps = code.chars.inject([[0, 0]]) do |acc, c|
      position, floor = acc.last
      increment = c == '(' ? 1 : -1
      [*acc, [position + 1, floor + increment]]
    end

    steps.find { _1.last < 0 }.first
  end
end
