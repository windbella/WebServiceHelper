### GZIP Response

.NET 웹서비스 (ASMX)를 이용하여 JSON이나 XML을 Response하게되는 API를 구성하는 경우가 많다.

웹서버 자체에 GZIP 기능이 있는 경우도 있지만 그렇지 않은 경우도 있기 때문에

간단하게 설정할수 있는 라이브러리를 만들어 보았다.

```
public static void Response(this HttpContext self, object content)
public static void Response(this HttpContext self, object content, string contentType, Encoding encoding)

Context.Response("{ a : 1 }");
```

위와 같은 방법으로 간단하게 사용할 수 있다.
