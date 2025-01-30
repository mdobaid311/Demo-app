import validator from "validator";
import { z } from "zod";

export const userFormSchema = z
  .object({
    FirstName: z
      .string()
      .min(1, {
        message: "Please enter a valid first name",
      })
      .default("")
      .superRefine((value, context) => {
        if (value.trim().length === 0) {
          context.addIssue({
            code: z.ZodIssueCode.custom,
            message: "Please enter a valid first name",
          });
        }
      }),
    LastName: z
      .string()
      .min(1, {
        message: "Please enter a valid last name",
      })
      .default("")
      .superRefine((value, context) => {
        if (value.trim().length === 0) {
          context.addIssue({
            code: z.ZodIssueCode.custom,
            message: "Please enter a valid last name",
          });
        }
      }),
    Email: z
      .string()
      .min(1, {
        message: "Please enter a valid email address",
      })
      .default("")
      .superRefine((value, context) => {
        if (value && !validator.isEmail(value)) {
          return context.addIssue({
            code: z.ZodIssueCode.custom,
            message: "Please enter a valid email address",
          });
        }
        if (value.trim().length === 0) {
          context.addIssue({
            code: z.ZodIssueCode.custom,
            message: "Please enter a valid email address",
          });
        }

        return;
      }),
    PhoneNumber: z.string().min(1, {
      message: "Please enter a valid phone number",
    }),
    Role: z.string().min(1, {
      message: "Please select a role",
    }),
    Password: z.string().min(1, {
      message: "Please enter a valid password",
    }),
  })
  .required({
    FirstName: true,
    LastName: true,
    Email: true,
    PhoneNumber: true,
    Role: true,
    Password: true,
  });

export const employeeFormSchema = z
  .object({
    FirstName: z
      .string()
      .min(1, {
        message: "Please enter a valid first name",
      })
      .default("")
      .superRefine((value, context) => {
        if (value.trim().length === 0) {
          context.addIssue({
            code: z.ZodIssueCode.custom,
            message: "Please enter a valid first name",
          });
        }
      }),
    LastName: z
      .string()
      .min(1, {
        message: "Please enter a valid last name",
      })
      .default("")
      .superRefine((value, context) => {
        if (value.trim().length === 0) {
          context.addIssue({
            code: z.ZodIssueCode.custom,
            message: "Please enter a valid last name",
          });
        }
      }),
    Email: z
      .string()
      .min(1, {
        message: "Please enter a valid email address",
      })
      .default("")
      .superRefine((value, context) => {
        if (value && !validator.isEmail(value)) {
          return context.addIssue({
            code: z.ZodIssueCode.custom,
            message: "Please enter a valid email address",
          });
        }
        if (value.trim().length === 0) {
          context.addIssue({
            code: z.ZodIssueCode.custom,
            message: "Please enter a valid email address",
          });
        }

        return;
      }),
    PhoneNumber: z.string().min(1, {
      message: "Please enter a valid phone number",
    }),
  })
  .required({
    FirstName: true,
    LastName: true,
    Email: true,
    PhoneNumber: true,
  });

export const loginFormSchema = z.object({
  Email: z.string().min(1, {
    message: "Please enter a valid email address",
  }),
  Password: z.string().min(1, {
    message: "Please enter a valid password",
  }),
});
