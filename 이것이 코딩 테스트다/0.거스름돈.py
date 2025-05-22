# 총액 1250
# 500, 100, 50, 10 단위
# 출력 :동전의 최소 갯수를 구해라

total_mony = 1250

coins = [500,100,50,10]
result = 0


for coin in coins:
    coin_change_count = total_mony // coin     
    result += coin_change_count
    total_mony %= coin

print(result)