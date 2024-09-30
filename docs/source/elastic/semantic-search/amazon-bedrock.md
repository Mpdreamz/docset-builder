---
title: "Semantic search with the inference API"
---

Semantic search helps you find data based on the intent and contextual meaning of a search query, instead of a match on query terms (lexical search).

In this tutorial, learn how to use the inference API workflow with various services to perform semantic search on your data.


```{admonition} Select your service

{bdg-link-primary}`Amazon Bedrock <amazon-bedrock.html>`
{bdg-link-primary-line}`Azure AI Studio <azure-ai-studio.html>`
{bdg-link-primary-line}`Azure OpenAI <azure-openai.html>`
{bdg-link-primary-line}`Cohere <cohere.html>`
{bdg-link-primary-line}`ELSER <elser.html>`
{bdg-link-primary-line}`HuggingFace <#>`
{bdg-link-primary-line}`Mistral <#>`
{bdg-link-primary-line}`OpenAI <#>`
{bdg-link-primary-line}`Service Alpha <#>`
{bdg-link-primary-line}`Service Bravo <#>`
{bdg-link-primary-line}`Service Charlie <#>`
{bdg-link-primary-line}`Service Delta <#>`
{bdg-link-primary-line}`Service Echo <#>`
{bdg-link-primary-line}`Service Foxtrot <#>`
```

----

## Amazon Bedrock

The examples in this tutorial use the `amazon.titan-embed-text-v1` model from the [Amazon Bedrock base models](https://docs.aws.amazon.com/bedrock/latest/userguide/model-ids.html).

## Requirements

* An AWS Account with [Amazon Bedrock](https://aws.amazon.com/bedrock/) access
* A pair of access and secret keys used to access Amazon Bedrock

## Create an inference endpoint

```{include} snippets/inference-endpoint.md
```

```{code-block} bash
:linenos:
:caption: Create inference example for `Amazon Bedrock`
:emphasize-lines: 3-7
PUT _inference/text_embedding/amazon_bedrock_embeddings
{
    "service": "amazonbedrock",
    "service_settings": {
        "access_key": "<aws_access_key>",
        "secret_key": "<aws_secret_key>",
        "region": "<region>",
        "provider": "<provider>",
        "model": "<model_id>"
    }
}
```

* The task type is `text_embedding` in the path and the `inference_id` which is the unique identifier of the inference endpoint is `amazon_bedrock_embeddings`.
* The access key can be found on your AWS IAM management page for the user account to access Amazon Bedrock.
* The secret key should be the paired key for the specified access key.
* Specify the region that your model is hosted in.
* Specify the model provider.
* The model ID or ARN of the model to use.

## Create the index mapping

```{include} snippets/index-mapping.md
```

```{code-block} bash
:linenos:
:caption: Create index mapping for `Amazon Bedrock`
:emphasize-lines: 6-12
PUT amazon-bedrock-embeddings
{
  "mappings": {
    "properties": {
      "content_embedding": {
        "type": "dense_vector",
        "dims": 1024,
        "element_type": "float",
        "similarity": "dot_product"
      },
      "content": {
        "type": "text"
      }
    }
  }
}
```

* The name of the field to contain the generated tokens. It must be referenced in the inference pipeline configuration in the next step.
* The field to contain the tokens is a `dense_vector` field.
* The output dimensions of the model. This value may be different depending on the underlying model used. See the [Amazon Titan model](https://docs.aws.amazon.com/bedrock/latest/userguide/titan-multiemb-models.html) or the [Cohere Embeddings model](https://docs.cohere.com/reference/embed) documentation.
* For Amazon Bedrock embeddings, the `dot_product` function should be used to calculate similarity for Amazon titan models, or `cosine` for Cohere models.
* The name of the field from which to create the dense vector representation. In this example, the name of the field is `content`. It must be referenced in the inference pipeline configuration in the next step.
* The field type which is text in this example.