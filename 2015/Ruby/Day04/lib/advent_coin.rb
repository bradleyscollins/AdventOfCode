# frozen_string_literal: true

require 'digest'

class AdventCoin
  def find_number(key, num_of_leading_zeros)
    leading_zeros = '0' * num_of_leading_zeros
    (1..).lazy
      .map  { { number: _1, md5: Digest::MD5.hexdigest("#{key}#{_1}") } }
      .find { _1[:md5].start_with? leading_zeros }
      .dig(:number)
  end
end
