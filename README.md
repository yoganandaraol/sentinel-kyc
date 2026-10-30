# sentinel-kyc

KYC means "know your customer". Banks, brokers and crypto exchanges do it before they take someone on, and keep doing it afterwards.

| Stage | What it answers | Your list |
|---|---|---|
| **1. Identify** | Who says they are this person? | Partly (point 1) |
| **2. Verify** | Is the identity real, and does it belong to this person? | Points 1–4 |
| **3. Screen** | Are they sanctioned, a politically exposed person (PEP), or in adverse media? | Missing |
| **4. Assess risk** | How risky is this customer? Low risk gets basic checks, high risk gets enhanced due diligence | Missing |
| **5. Decide** | Approve, reject, ask for more, or send to a human | Missing |
| **6. Monitor** | Periodic reviews and re-screening as lists and circumstances change | Missing |
| **7. Communicate and record** | Tell the customer what is needed, and keep an audit trail | Point 5, partly |

Two corrections to your list:
- **Two government documents** is not a universal rule. It varies by country and by each institution's risk policy. Model it as configurable policy, not hard-coded logic. In Australia the regulator is AUSTRAC.
- **Verifying against trusted sources** (point 4) normally means asking a government or bureau service whether the document is genuine. You can't get access to those, so you build an adapter with the same request and response shape and mock it with synthetic data.

## What it looks like in practice

```
Customer applies, uploads ID and a selfie
  → Intake: what is missing or unclear?
  → Documents: extract fields, check consistency, check expiry
  → Verify: match against a source (mocked), compare selfie to ID photo (mocked)
  → Screening: sanctions, PEP, adverse media (parallel)
  → Risk: score plus reasons
  → Decision: auto-approve low risk, or send to an analyst with a case summary
  → Notify the customer, and log every step
```

Customers see a status page and emails. Analysts see a queue of cases with a summary, evidence, the agents' reasoning, and approve, reject or request-more buttons. That is your P8 console.

## The real problems classic KYC has (your opportunity)

1. **Customers drop out** because forms are long and rejections are vague.
2. **Manual review backlogs** take days, and analysts re-read the same documents.
3. **Name-screening false positives**: most alerts are false matches ("John Smith" against a list), and analysts burn hours clearing them.
4. **Inconsistent decisions** between analysts on similar cases.
5. **Fraud**: forged documents, deepfakes and synthetic identities.
6. **Hard to explain to auditors** why a case was approved.
7. **Customers get asked for the same thing twice**, or don't know what is missing.

Agents help most with 2, 3, 6 and 7: summarising cases, triaging screening hits, explaining decisions and drafting clear requests. That is the strongest story for your portfolio.

## What to build and what to mock

You have about 80 hours, so don't build OCR, face matching or liveness yourself.

- **Build for real:** the case lifecycle, the workflow, the policy rules, screening triage, risk scoring with reasons, the analyst console, the audit trail, the evals, and guardrails.
- **Wrap behind an interface and mock first:** document extraction, selfie-to-ID comparison, liveness, and verification against the trusted source. You can plug in a real service later, such as Azure AI Document Intelligence. Some face features there need approval from Microsoft.
- **Video comparison:** leave it as a stretch goal. Define the interface now and don't implement it.

## A real-world rule to build in

In AML law you must not tip off a customer that they are suspected of money laundering (in Australia, under the AML/CTF Act). So the email-drafting agent should use approved templates, avoid sanctions or suspicion wording, and be tested on exactly that. This is a good example of a real problem that most demos skip.

## Draft for Day 8

Write one page covering:
1. **Actors:** customer, analyst, compliance manager.
2. **Case states:** submitted, docs requested, verifying, screening, assessed, in review, approved, rejected, on hold.
3. **Decision outcomes:** approve, reject, request more info, refer.
4. **Policy config:** which documents are accepted, thresholds, who must review high risk.
5. **Agents and the tools each may use.**
6. **Audit fields:** who or what did what, when, with which inputs.
7. **Non-goals:** real OCR, real face matching.

Do you want me to write this up as a requirements doc (a document you can keep and edit) and link it from the P1 playbook? I'd also add a short glossary of KYC terms, since this is new to you.
