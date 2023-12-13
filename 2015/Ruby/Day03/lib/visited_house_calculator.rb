# frozen_string_literal: true

class VisitedHouseCalculator
  def initialize(initial_position)
    @initial_position = initial_position
  end

  def calculate(moves)
    path = moves.chars
      .map(&method(:move_to_offset))
      .reduce([@initial_position]) do |visited, offset|
        x, y = visited.last
        δx, δy = offset
        [*visited, [x + δx, y + δy]]
      end

    path.uniq.count
  end

private

  def move_to_offset(move)
    case move
    when '^' then [ 0,  1]
    when 'v' then [ 0, -1]
    when '>' then [ 1,  0]
    when '<' then [-1,  0]
    else          [ 0,  0]
    end
  end
end
