// import React from 'react';


// const validationSchema = yup.object({
//     firstName: yup.string().required().max(10),
//   });

// export type FormFieldBaseProps<Value> = {
//   name: string;
//   onBlur: () => void;
//   onChange: (value: Value) => void;
//   value: Value;
// };

// export type FormFieldExtraProps = {
//   error: string | undefined;
//   touched: boolean;
// };

// export type FormFieldProps<Value> = FormFieldBaseProps<Value> & FormFieldExtraProps;

// export type FormFieldPropsValue<P extends FormFieldProps<any>> = P extends FormFieldProps<infer V> ? V : never;


// export function withoutAdditionalProps<P>(props: P) {
//   const { error, touched, ...rest } = (props as unknown) as FormFieldProps<any>;
//   return (rest as unknown) as Omit<P, 'error' | 'touched'>;
// }

// export type RadioProps<T> = Omit<
//   React.HTMLProps<HTMLInputElement>,
//   'onBlur' | 'onChange' | 'value' | 'checked' | 'type' | 'ref'
// > & {
//   checkValue: T;
//   inputRef?: React.Ref<HTMLInputElement>;
// } & FormFieldBaseProps<T>;

// export const Radio = <T extends string | number | undefined>({
//   onBlur,
//   onChange,
//   checkValue,
//   inputRef,
//   value,
//   ...props
// }: RadioProps<T>) => {
//   return (
//     <input
//       {...withoutAdditionalProps(props)}
//       checked={checkValue === value}
//       type="radio"
//       ref={inputRef}
//       onBlur={() => onBlur()}
//       onChange={() => onChange(checkValue)}
//       value={value ?? ''}
//     />
//   );
// };

// //next file

// type Props<T> = FormFieldProps<T> & {
//     radios: {
//       checkValue: T;
//       label: string;
//     }[];
//   };
  
//   export const RadioGroup = <T extends string | number | undefined>({
//     touched,
//     error,
//     radios,
//     ...radioProps
//   }: Props<T>) => {
//     const hasError = touched && error;
//     return (
//       <div
//         className={cn('form-group', {
//           'has-error': hasError,
//         })}
//       >
//         {radios.map(({ checkValue, label }) => (
//           <div key={checkValue} className="radio">
//             <label>
//               <Radio checkValue={checkValue} {...radioProps} />
//               {label}
//             </label>
//           </div>
//         ))}
        
//       </div>
//     );
//   };

//   //next file
  
//   <RadioGroup
//               {...fields.gender}
//               radios={[
//                 { checkValue: GenderType.Female, label: 'Female' },
//                 { checkValue: GenderType.Male, label: 'Male' },
//               ]}
//             />

// const errorStyle = {
//     color: "red",
//     "font-size": "10px",
//     "font-style": "italic",
//   };
  
//   const MyTextField: React.FC<FieldAttributes<any>> = ({
//     placeholder,
//     ...props
//   }) => {
//     const [field, meta] = useField(props);
//     const errorText = meta.error && meta.touched ? meta.error : "";
  
//     return (
//       <div>
//         <Input {...field} placeholder={placeholder} error={!!errorText} />
//         <p style={errorStyle}>{errorText}</p>
//       </div>
//     );
//   };
  
//   type MyCheckboxProps = FieldConfig<any>;
  
//   const CheckboxTall: React.FC<MyCheckboxProps> = ({ ...props }) => {
//     const [field] = useField<any>(props);
  
//     return (
//       <Checkbox label="Is tall?" id={field.name} onChange={field.onChange} />
//     );
//   };
  
//   const CheckboxCookies: React.FC<MyCheckboxProps> = ({ ...props }) => {
//     const [field] = useField<any>(props);
  
//     return (
//       <Checkbox
//         label={field.value}
//         id={field.value}
//         name={field.name}
//         onChange={field.onChange}
//         value={field.value}
//       />
//     );
//   };
  