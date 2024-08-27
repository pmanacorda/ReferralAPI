if [[ "$1" != "" ]]; then
    TOKEN="$1"
else
    TOKEN=.
fi

echo "\n \n"

echo "TEST: GET LINK"
curl -v "http://localhost:5010/api/referral/link" -H "Authorization: Bearer $TOKEN" | jq

echo "\n \n"

echo "TEST: GET HISTORY"
curl -v "http://localhost:5010/api/referral/history" -H "Authorization: Bearer $TOKEN" | jq

echo "\n \n"

echo "TEST: VALIDATE TOKEN"
curl -v "http://localhost:5010/api/referral/validate?token=JwbWFuYWNvcmRhI" | jq

echo "\n \n"