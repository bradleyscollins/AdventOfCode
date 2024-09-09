# frozen_string_literal: true

class FloorDecoder
  def decode(code)
    code.chars.sum { _1 == '(' ? 1 : -1 }
  end
end
