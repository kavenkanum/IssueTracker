import React, { useState, useEffect } from "react";
import {
  Container,
  Header,
  Form,
  Button,
  Dropdown,
  DropdownItemProps,
  Input,
  Checkbox,
  Radio,
  Item,
} from "semantic-ui-react";
import { Select, MenuItem } from "@material-ui/core";
import {
  Formik,
  Field,
  FieldArray,
  SharedRenderProps,
  FieldArrayRenderProps,
  FieldAttributes,
  useField,
  FieldConfig,
} from "formik";
import * as yup from "yup";

export const MyRadioGroup: React.FC<MyCheckboxProps> = ({ ...props }) => {
  const [field, meta] = useField<any>(props);

  return (
    <RadioGroup
      {...field}
      error={meta.error}
      touched={meta.touched}
      radios={[
        { checkValue: FruitType.Pineapple, label: "Pineapple" },
        { checkValue: FruitType.Apple, label: "Apple" },
        { checkValue: FruitType.Banana, label: "Banana" },
      ]}
    />
  );
};

type MyCheckboxProps = FieldConfig<any>;

const RadioGroup = <T extends string | number | undefined>({
  touched,
  error,
  radios,
  ...radioProps
}: Props<T>) => {
  const hasError = touched && error;
  return (
    <div>
      {radios.map(({ checkValue, label }) => (
        <div key={checkValue} className="radio">
          <label>
            <MyRadio checkValue={checkValue} {...radioProps} />
            {label}
          </label>
        </div>
      ))}
    </div>
  );
};

const MyRadio = <T extends string | number | undefined>({
  onBlur,
  onChange,
  checkValue,
  inputRef,
  value,
  ...props
}: RadioProps<T>) => {
  return (
    <input
      checked={checkValue === value}
      type="radio"
      ref={inputRef}
      onChange={() => onChange(checkValue)}
      value={value ?? ""}
    />
  );
};

type RadioProps<T> = Omit<
  React.HTMLProps<HTMLInputElement>,
  "onBlur" | "onChange" | "value" | "checked" | "type" | "ref"
> & {
  checkValue: T;
  inputRef?: React.Ref<HTMLInputElement>;
} & FormFieldBaseProps<T>;

type Props<T> = FormFieldProps<T> & {
  radios: {
    checkValue: T;
    label: string;
  }[];
};

type FormFieldProps<Value> = FormFieldBaseProps<Value> & FormFieldExtraProps;

type FormFieldBaseProps<Value> = {
  name: string;
  onBlur: {
    (e: FocusEvent): void;
    <T = any>(fieldOrEvent: T): T extends string ? (e: any) => void : void;
  };
  onChange: (value: Value) => void;
  value: Value;
};

type FormFieldExtraProps = {
  error: string | undefined;
  touched: boolean;
};

enum FruitType {
  None = 0,
  Pineapple = 1,
  Apple = 2,
  Banana = 3,
}

type SomeData = {
  firstName: string;
  lastName: string;
  isTall: boolean;
  cookies: string[];
  fruit: FruitType;
  pets: [{ id: number; type: string; name: string }];
};

const validationSchema = yup.object({
  firstName: yup.string().required().max(10),
});

export const AddPrevJob: React.FC = () => {
  return (
    <Container>
      <Formik<SomeData>
        initialValues={{
          firstName: "",
          lastName: "",
          isTall: false,
          cookies: [],
          fruit: FruitType.None,
          pets: [{ id: 1, type: "dog", name: "Szejk" }],
        }}
        onSubmit={(data, { setSubmitting }) => {
          setSubmitting(true);
          console.log(data);
          setSubmitting(false);
        }}
        validationSchema={validationSchema}
        // validate={(values) => {
        //   const errors: Record<string, string> = {};

        //   if (values.firstName.length == 0) {
        //     errors.firstName = "First name cannot be empty";
        //   }
        //   return errors;
        // }}
      >
        {({ values, errors, isSubmitting, handleSubmit }) => (
          <form onSubmit={handleSubmit}>
            <div>
              <MyTextField
                placeholder="First name"
                name="firstName"
                type="input"
              />
            </div>
            <div>
              <Field
                placeholder="Last name"
                name="lastName"
                type="input"
                as={Input}
              />
            </div>
            <div>
              <Field name="isTall" type="checkbox" as={CheckboxTall} />
            </div>
            <div>
              <div>Cookies:</div>
              <Field
                name="cookies"
                type="checkbox"
                value="chocolate"
                as={CheckboxCookies}
              />
              <Field
                name="cookies"
                type="checkbox"
                value="white chocolate"
                as={CheckboxCookies}
              />
              <Field
                name="cookies"
                type="checkbox"
                value="snickers"
                as={CheckboxCookies}
              />
            </div>
            <div>
              <div>Fruit</div>
              <div>
                <Field name="fruit" type="radio" value="pineapple" />
                <Field name="fruit" type="radio" value="apple" />
                <Field name="fruit" type="radio" value="banana" />
              </div>
              {/* <MyRadioGroup name="fruit" type="radio" value={values.fruit} /> */}
            </div>
            <FieldArray name="pets">
              {(arrayHelpers) => (
                <div>
                  <Button
                    onClick={() =>
                      arrayHelpers.push({
                        id: "" + Math.random(),
                        type: "frog",
                        name: "",
                      })
                    }
                  >
                    Add pet
                  </Button>
                  {values.pets.map((pet, index) => {
                    const petName = `pets.${index}.name`;
                    const petType = `pets.${index}.type`;
                    return (
                      <div key={pet.id}>
                        <MyTextField placeholder="pet name" name={petName} />
                        <Field name={petType} type="select" as={Select}>
                          <MenuItem value="dog">Dog</MenuItem>
                          <MenuItem value="cat">Cat</MenuItem>
                          <MenuItem value="fish">Fish</MenuItem>
                        </Field>
                        <Button onClick={() => arrayHelpers.remove(index)}>
                          X
                        </Button>
                      </div>
                    );
                  })}
                </div>
              )}
            </FieldArray>
            <Button disabled={isSubmitting} type="submit">
              Submit
            </Button>
            <pre>{JSON.stringify(values, null, 2)}</pre>
            <pre>{JSON.stringify(errors, null, 2)}</pre>
          </form>
        )}
      </Formik>
    </Container>
  );
};

const errorStyle = {
  color: "red",
  "font-size": "10px",
  "font-style": "italic",
};

const MyTextField: React.FC<FieldAttributes<any>> = ({
  placeholder,
  ...props
}) => {
  const [field, meta] = useField(props);
  const errorText = meta.error && meta.touched ? meta.error : "";

  return (
    <div>
      <Input {...field} placeholder={placeholder} error={!!errorText} />
      <p style={errorStyle}>{errorText}</p>
    </div>
  );
};

const CheckboxTall: React.FC<MyCheckboxProps> = ({ ...props }) => {
  const [field] = useField<any>(props);

  return (
    <Checkbox label="Is tall?" id={field.name} onChange={field.onChange} />
  );
};

const CheckboxCookies: React.FC<MyCheckboxProps> = ({ ...props }) => {
  const [field] = useField<any>(props);

  return (
    <Checkbox
      label={field.value}
      id={field.value}
      name={field.name}
      onChange={field.onChange}
      value={field.value}
    />
  );
};
