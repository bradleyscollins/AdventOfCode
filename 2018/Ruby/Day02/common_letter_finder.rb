module ArrayRefinement
  refine Array do
    def cartesian(other)
      self.flat_map { |x| other.map { |y| [x, y] } }
    end
  end
end

module StringRefinement
  refine String do
    def zip(other)
      self.chars.zip(other.chars)
    end
  end
end

class CommonLetterFinder
  using ArrayRefinement
  using StringRefinement

  def find(codes)
    codes.cartesian(codes)
      .lazy
      .reject { |code1, code2| code1 == code2 }
      .select { |code1, code2| diffBy1(code1, code2) }
      .map { |code1, code2| keepMatchingChars(code1, code2) }
      .uniq
      .first
  end

  private
    def diffBy1(code1, code2)
      code1.zip(code2).one? { |a,b| a != b }
    end

    def keepMatchingChars(code1, code2)
      code1.zip(code2).reject { |a,b| a != b }.map { |c,_| c }.join
    end
end
