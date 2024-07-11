using Lu.Ma.Extensions;
using Lu.Ma.Models;
using System.Text.Json;

namespace Lu.Ma.Tests;

public class SerializationTests
{
    public static readonly string SampleEventGuestJsonResponse = """
        {
          "entries": [
            {
              "api_id": "abc-123",
              "guest": {
                "api_id": "abc-123",
                "approval_status": "declined",
                "created_at": "2024-06-10T17:29:03.804Z",
                "custom_source": null,
                "eth_address": null,
                "invited_at": null,
                "joined_at": null,
                "phone_number": "+15555555555",
                "registered_at": "2024-06-10T17:29:03.803Z",
                "registration_answers": [
                  {
                    "label": "What is your Instagram username?",
                    "answer": "http://example.com",
                    "question_id": "ta75qpw2",
                    "question_type": "instagram"
                  },
                  {
                    "label": "What is your TikTok handle?",
                    "answer": "testaccount",
                    "question_id": "8w4dlaex",
                    "question_type": "url"
                  },
                  {
                    "label": "Preferred Shirt/Hoodie Size",
                    "answer": "M",
                    "question_id": "vwhu7ffr",
                    "question_type": "dropdown"
                  },
                  {
                    "label": "Shipping Address",
                    "answer": "123 Test Street",
                    "question_id": "ahpjn7ux",
                    "question_type": "long-text"
                  },
                  {
                    "label": "Interests",
                    "answer": [
                      "Fashion / Beauty"
                    ],
                    "question_id": "axfnx8yn",
                    "question_type": "multi-select"
                  },
                  {
                    "label": "This is a test label",
                    "answer": true,
                    "question_id": "ysuza5s0",
                    "question_type": "agree-check"
                  }
                ],
                "solana_address": null,
                "user_api_id": "usr-CM21EjMYbRvQ2Ck",
                "user_name": "Bob Jones",
                "user_email": "bob@jones.com",
                "name": "Bob Jones",
                "email": "bob@jones.com",
                "checked_in_at": null,
                "check_in_qr_code": "https://lu.ma/check-in/evt-abc123?pk=g-abc123",
                "event_ticket": null
              }
            },
            {
              "api_id": "abc-123",
              "guest": {
                "api_id": "abc-123",
                "approval_status": "approved",
                "created_at": "2024-06-10T19:11:13.181Z",
                "custom_source": null,
                "eth_address": null,
                "invited_at": "2024-06-10T19:11:13.180Z",
                "joined_at": null,
                "phone_number": "+15555555555",
                "registered_at": "2024-07-08T17:03:49.605Z",
                "registration_answers": [
                  {
                    "label": "What is your Instagram username?",
                    "answer": "http://example.com",
                    "question_id": "ta75qpw2",
                    "question_type": "instagram"
                  },
                  {
                    "label": "What is your TikTok handle?",
                    "answer": "testaccount",
                    "question_id": "8w4dlaex",
                    "question_type": "url"
                  },
                  {
                    "label": "What is your Twitter handle?",
                    "answer": "https://x.com/contessaboorman",
                    "question_id": "0lb2tds9",
                    "question_type": "url"
                  },
                  {
                    "label": "Other social handle(s)",
                    "answer": "",
                    "question_id": "5rjuuptw",
                    "question_type": "url"
                  },
                  {
                    "label": "Preferred Shirt/Hoodie Size",
                    "answer": "M",
                    "question_id": "vwhu7ffr",
                    "question_type": "dropdown"
                  },
                  {
                    "label": "Shipping Address",
                    "answer": "123 Test Street",
                    "question_id": "ahpjn7ux",
                    "question_type": "long-text"
                  },
                  {
                    "label": "Interests",
                    "answer": [],
                    "question_id": "axfnx8yn",
                    "question_type": "multi-select"
                  },
                  {
                    "label": "Who should we thank for inviting you?",
                    "answer": "",
                    "question_id": "1wr9v5nf",
                    "question_type": "text"
                  },
                  {
                    "label": "This is a test label",
                    "answer": true,
                    "question_id": "ysuza5s0",
                    "question_type": "agree-check"
                  }
                ],
                "solana_address": null,
                "user_api_id": "usr-9E2KaeIyCFXLmpE",
                "user_name": "Bob Jones",
                "user_email": "bob@jones.com",
                "name": "Bob Jones",
                "email": "bob@jones.com",
                "checked_in_at": null,
                "check_in_qr_code": "https://lu.ma/check-in/evt-abc123?pk=g-abc123",
                "event_ticket": {
                  "amount": 0,
                  "amount_discount": 0,
                  "api_id": "abc-123",
                  "currency": null,
                  "event_ticket_type_api_id": "evtticktyp-abc123",
                  "name": "Free"
                }
              }
            },
            {
              "api_id": "abc-123",
              "guest": {
                "api_id": "abc-123",
                "approval_status": "approved",
                "created_at": "2024-06-11T14:33:48.721Z",
                "custom_source": null,
                "eth_address": null,
                "invited_at": null,
                "joined_at": null,
                "phone_number": "+15555555555",
                "registered_at": "2024-06-11T14:33:48.720Z",
                "registration_answers": [
                  {
                    "label": "What is your Instagram username?",
                    "answer": "http://example.com",
                    "question_id": "ta75qpw2",
                    "question_type": "instagram"
                  },
                  {
                    "label": "What is your TikTok handle?",
                    "answer": "testaccount",
                    "question_id": "8w4dlaex",
                    "question_type": "url"
                  },
                  {
                    "label": "Preferred Shirt/Hoodie Size",
                    "answer": "L",
                    "question_id": "vwhu7ffr",
                    "question_type": "dropdown"
                  },
                  {
                    "label": "Shipping Address",
                    "answer": "123 Test Street",
                    "question_id": "ahpjn7ux",
                    "question_type": "long-text"
                  },
                  {
                    "label": "Interests",
                    "answer": [
                      "Comedy / Entertainment"
                    ],
                    "question_id": "axfnx8yn",
                    "question_type": "multi-select"
                  },
                  {
                    "label": "This is a test label",
                    "answer": true,
                    "question_id": "ysuza5s0",
                    "question_type": "agree-check"
                  }
                ],
                "solana_address": null,
                "user_api_id": "usr-TEChjgCOMbL3nF4",
                "user_name": "Bob Jones",
                "user_email": "bob@jones.com",
                "name": "Bob Jones",
                "email": "bob@jones.com",
                "checked_in_at": null,
                "check_in_qr_code": "https://lu.ma/check-in/evt-abc123?pk=g-abc123",
                "event_ticket": {
                  "amount": 0,
                  "amount_discount": 0,
                  "api_id": "abc-123",
                  "currency": null,
                  "event_ticket_type_api_id": "evtticktyp-abc123",
                  "name": "Free"
                }
              }
            }
          ],
          "has_more": true,
          "next_cursor": "gst-abc123"
        }
        """;

    [Fact]
    public void Should_Serialize()
    {
        var result = JsonSerializer
            .Deserialize<GetEventGuestResponse>(SampleEventGuestJsonResponse, HttpClientExtensions.JsonOptions);

        Assert.NotNull(result);
    }
}