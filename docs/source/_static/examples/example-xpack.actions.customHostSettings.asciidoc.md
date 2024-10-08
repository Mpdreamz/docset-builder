In the following example, two custom host settings are defined.  The first 
provides a custom host setting for mail server `mail.example.com` using 
port 465 that supplies server certificate authentication data from both 
a file and inline, and requires TLS for the connection.  The second provides 
a custom host setting for https server `webhook.example.com` which turns off 
server certificate authentication, that will allow Kibana to connect to the 
server if it's using a self-signed certificate. The individual properties 
that can be used in the settings are documented below.

```yaml
  xpack.actions.customHostSettings:
    - url: smtp://mail.example.com:465
      ssl:
        verificationMode: 'full'
        certificateAuthoritiesFiles: [ 'one.crt' ]
        certificateAuthoritiesData: |
            -----BEGIN CERTIFICATE-----
            MIIDTD...
            CwUAMD...
            ... multiple lines of certificate data ...
            -----END CERTIFICATE-----
      smtp:
        requireTLS: true
    - url: https://webhook.example.com
      ssl:
        verificationMode: 'none'
  ```