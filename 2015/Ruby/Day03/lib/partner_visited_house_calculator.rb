# frozen_string_literal: true

class PartnerVisitedHouseCalculator
  def initialize(partners:, initial_position:)
    @initial_position = initial_position
    @partners         = partners
  end

  def calculate(moves)
    offset_sets = moves.chars
      .map(&method(:move_to_offset))
      .group_by.with_index { |c,i| i % @partners }
      .values

    paths = offset_sets.map(&method(:calc_path))
    houses = paths.flatten(1)
    houses.uniq.count
  end

private

  def calc_path(offsets)
    offsets.reduce([@initial_position]) do |visited, offset|
      x, y = visited.last
      δx, δy = offset
      [*visited, [x + δx, y + δy]]
    end
  end

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
